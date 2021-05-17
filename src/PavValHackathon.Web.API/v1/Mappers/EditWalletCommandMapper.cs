using PavValHackathon.Web.API.v1.Commands.Wallets;
using PavValHackathon.Web.API.v1.Contracts.Requests;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class EditWalletCommandMapper : IMapperDefinition<EditWalletRequestContract, EditWalletCommand>
    {
        public EditWalletCommand Map(EditWalletRequestContract @from)
        {
            throw new System.NotImplementedException();
        }

        public EditWalletCommand Map(EditWalletCommand command, EditWalletRequestContract contract)
        {
            Assert.IsNotNull(command, nameof(command));
            Assert.IsNotNull(contract, nameof(contract));

            command.Title = contract.Title;
            command.CurrencyId = contract.CurrencyId;
            
            return command;
        }
    }
}