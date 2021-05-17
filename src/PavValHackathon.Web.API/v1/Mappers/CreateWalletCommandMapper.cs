using PavValHackathon.Web.API.v1.Commands.Wallets;
using PavValHackathon.Web.API.v1.Contracts.Requests;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class CreateWalletCommandMapper : IMapperDefinition<CreateWalletRequestContract, CreateWalletCommand>
    {
        public CreateWalletCommand Map(CreateWalletRequestContract contract)
        {
            Assert.IsNotNull(contract, nameof(contract));

            return Map(new CreateWalletCommand(), contract);
        }

        public CreateWalletCommand Map(CreateWalletCommand command, CreateWalletRequestContract contract)
        {
            Assert.IsNotNull(command, nameof(command));
            Assert.IsNotNull(contract, nameof(contract));

            command.Title = contract.Title;
            command.CurrencyId = contract.CurrencyId;
            
            return command;
        }
    }
}