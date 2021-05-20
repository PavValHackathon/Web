using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.Infrastructure.Extensions;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories;
using PavValHackathon.Web.Data.Repositories.Custom;
using Void = PavValHackathon.Web.Common.Void;

namespace PavValHackathon.Web.API.v1.Commands.Wallets.Handlers
{
    public class DeleteWalletCommandHandler : ICommandHandler<DeleteWalletCommand, Void>
    {
        private readonly IUserContext _userContext;
        private readonly IWalletRepository _walletRepository;
        private readonly IRepository<Bucket> _bucketRepository;
        private readonly IRepository<Transaction> _transactionRepository;

        public DeleteWalletCommandHandler(
            IUserContext userContext,
            IWalletRepository walletRepository,
            IRepository<Bucket> bucketRepository,
            IRepository<Transaction> transactionRepository)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _bucketRepository = bucketRepository ?? throw new ArgumentNullException(nameof(bucketRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        public async Task<Result<Void>> HandleAsync(DeleteWalletCommand command, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            var userId = _userContext.UserId;
            var wallet = await _walletRepository.GetAsync(command.Id, userId, cancellationToken);
            
            if (wallet is null)
                return this.NotFound("Wallet not found.");

            if (wallet.Transactions.Any())
                await RecalculateBucketsAsync(wallet.Transactions, cancellationToken);
            
            await _walletRepository.DeleteAsync(command.Id, cancellationToken);
            
            return this.Ok();
        }

        private async Task RecalculateBucketsAsync(ICollection<Transaction> transactions, CancellationToken cancellationToken)
        {
            var bucketMap = new Dictionary<int, Bucket>();
            
            foreach (var transaction in transactions)
            {
                if (!bucketMap.TryGetValue(transaction.BucketId, out var bucket))
                {
                    bucket = await _bucketRepository.GetAsync(transaction.BucketId, cancellationToken);
                    bucketMap.Add(transaction.BucketId, bucket!);
                }

                bucket!.Amount -= transaction.Value;
            }

            await _bucketRepository.UpdateManyAsync(bucketMap.Values, cancellationToken);
            await _transactionRepository.DeleteManyAsync(transactions, cancellationToken);
        }
    }
}