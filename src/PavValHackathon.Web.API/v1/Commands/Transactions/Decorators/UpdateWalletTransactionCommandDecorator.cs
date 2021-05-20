using System;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.Infrastructure.Extensions;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories.Custom;

namespace PavValHackathon.Web.API.v1.Commands.Transactions.Decorators
{
    public class UpdateWalletTransactionCommandDecorator<TCommand, TResult> : CommandHandlerDecorator<TCommand, TResult>
        where TCommand : TransactionCommand<TResult>
    {
        private readonly IUserContext _userContext;
        private readonly IWalletRepository _walletRepository;

        public UpdateWalletTransactionCommandDecorator(
            ICommandHandler<TCommand, TResult> innerHandler,
            IUserContext userContext, 
            IWalletRepository walletRepository) : base(innerHandler)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        }

        public override async Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            Wallet? wallet;
            
            var userId = _userContext.UserId;
            var walletCount = await _walletRepository.CountAsync(p => p.UserId == userId, cancellationToken);

            switch (walletCount)
            {
                case 0:
                    return this.BadRequest("User does not have wallet.");
                case > 1 when !command.WalletId.HasValue:
                    return this.BadRequest("User has more than 1 wallet. Please add 'walletId' to the request body.");
                case > 1:
                    wallet = await _walletRepository.GetAsync(command.WalletId.Value, userId, cancellationToken);
                    break;
                default:
                    wallet = await _walletRepository.GetAsync(p => p.UserId == userId, cancellationToken);
                    break;
            }

            if (wallet is null)
                return this.BadRequest("Wallet not found");

            command.WalletId = wallet.Id;
            
            var innerResult = await InnerHandler.HandleAsync(command, cancellationToken);

            if (innerResult.IsFailed)
                return innerResult;

            wallet.Amount += command.Value;

            await _walletRepository.UpdateAsync(wallet, cancellationToken);

            return innerResult;
        }
    }
}