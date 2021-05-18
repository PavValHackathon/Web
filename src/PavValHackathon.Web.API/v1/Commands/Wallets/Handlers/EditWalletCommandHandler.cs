using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories;
using PavValHackathon.Web.Data.Repositories.Custom;
using Void = PavValHackathon.Web.Common.Void;

namespace PavValHackathon.Web.API.v1.Commands.Wallets.Handlers
{
    public class EditWalletCommandHandler : ICommandHandler<EditWalletCommand, Void>
    {
        private readonly IUserContext _userContext;
        private readonly IWalletRepository _walletRepository;
        private readonly Lazy<IReadOnlyRepository<Currency>> _currencyRepository;

        public EditWalletCommandHandler(
            IUserContext userContext,
            IWalletRepository walletRepository,
            Lazy<IReadOnlyRepository<Currency>> currencyRepository)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
        }

        public async Task<Result<Void>> HandleAsync(EditWalletCommand command, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            var userId = _userContext.UserId;
            var wallet = await _walletRepository.GetAsync(command.Id, userId, cancellationToken);

            if (wallet is null)
                return Result.Failed<Void>((int) HttpStatusCode.NotFound, "Wallet not found.");

            if (!string.IsNullOrWhiteSpace(command.Title))
                wallet.Title = command.Title;

            if (command.CurrencyId.HasValue)
            {
                var currencyFound = await _currencyRepository.Value.ExistAsync(command.CurrencyId.Value, cancellationToken);

                if (!currencyFound)
                    return Result.Failed<Void>((int) HttpStatusCode.NotFound, "Currency not found.");
                
                wallet.CurrencyId = command.CurrencyId.Value;
            }

            await _walletRepository.UpdateAsync(wallet, cancellationToken);

            return Result.Ok(Void.Instance);
        }
    }
}