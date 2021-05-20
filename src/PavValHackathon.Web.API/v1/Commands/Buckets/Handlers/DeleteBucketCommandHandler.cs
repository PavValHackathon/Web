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

namespace PavValHackathon.Web.API.v1.Commands.Buckets.Handlers
{
    public class DeleteBucketCommandHandler : ICommandHandler<DeleteBucketCommand, Void>
    {
        private readonly IUserContext _userContext;
        private readonly IWalletRepository _walletRepository;
        private readonly IRepository<Bucket> _bucketRepository;
        private readonly IRepository<Transaction> _transactionRepository;

        public DeleteBucketCommandHandler(
            IUserContext userContext, 
            IRepository<Bucket> repository, 
            IWalletRepository walletRepository, 
            IRepository<Transaction> transactionRepository)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _bucketRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        public async Task<Result<Void>> HandleAsync(DeleteBucketCommand command, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            var userId = _userContext.UserId;
            var bucket = await _bucketRepository.GetAsync(p => p.Id == command.Id && p.UserId == userId, cancellationToken);

            if (bucket is null)
                return this.NotFound("Bucket not found.");

            if (bucket.Transactions.Any())
                await RecalculateWalletAsync(bucket.Transactions, cancellationToken);
            
            await _bucketRepository.DeleteAsync(bucket, cancellationToken);
            
            return this.Ok();
        }

        private async Task RecalculateWalletAsync(ICollection<Transaction> transactions, CancellationToken cancellationToken)
        {
            var walletIds = new HashSet<int>(transactions.Select(p => p.WalletId));
            var wallets = await _walletRepository.ListAsync(p => walletIds.Contains(p.Id), 100, 0, cancellationToken);
            var walletMap = wallets.ToDictionary(key => key.Id);

            foreach (var transaction in transactions)
            {
                walletMap[transaction.WalletId].Amount -= transaction.Value;
            }

            await _walletRepository.UpdateManyAsync(wallets, cancellationToken);
            await _transactionRepository.DeleteManyAsync(transactions, cancellationToken);
        }
    }
}