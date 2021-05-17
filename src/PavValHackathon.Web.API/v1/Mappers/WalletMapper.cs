using PavValHackathon.Web.API.v1.Commands.Wallets;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class WalletMapper : IMapperDefinition<CreateWalletCommand, Wallet>
    {
        public Wallet Map(CreateWalletCommand createWalletCommand)
        {
            Assert.IsNotNull(createWalletCommand, nameof(createWalletCommand));

            return Map(new Wallet(), createWalletCommand);
        }

        public Wallet Map(Wallet wallet, CreateWalletCommand createWalletCommand)
        {
            Assert.IsNotNull(wallet, nameof(wallet));
            Assert.IsNotNull(createWalletCommand, nameof(createWalletCommand));

            wallet.Title = createWalletCommand.Title;
            wallet.CurrencyId = createWalletCommand.CurrencyId;
            
            return wallet;
        }
    }
}