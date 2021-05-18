using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.Infrastructure.Extensions;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories;
using PavValHackathon.Web.Data.Repositories.Custom;

namespace PavValHackathon.Web.API.v1.Commands.Wallets.Handlers
{
    public class CreateWalletCommandHandler : ICommandHandler<CreateWalletCommand, int>
    {
        private readonly IUserContext _userContext;
        private readonly IWalletRepository _repository;
        private readonly IReadOnlyRepository<Currency> _currencyRepository;
        private readonly IMapperDefinition<CreateWalletCommand, Wallet> _walletMapper;

        public CreateWalletCommandHandler(
            IUserContext userContext,
            IWalletRepository repository,
            IReadOnlyRepository<Currency> currencyRepository,
            IMapperDefinition<CreateWalletCommand, Wallet> walletMapper)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _walletMapper = walletMapper ?? throw new ArgumentNullException(nameof(walletMapper));
            _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
        }

        public async Task<Result<int>> HandleAsync(CreateWalletCommand command, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            if (await _currencyRepository.ExistAsync(command.CurrencyId, cancellationToken) == false)
                return this.NotFound("Currency not found.");
            
            var domainWallet = _walletMapper.Map(command);
            domainWallet.UserId = _userContext.UserId;
            domainWallet = await _repository.CreateAsync(domainWallet, cancellationToken);

            return this.Ok(domainWallet.Id);
        }
    }
}