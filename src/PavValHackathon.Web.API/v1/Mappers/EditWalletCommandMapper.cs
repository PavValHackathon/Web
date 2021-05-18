using PavValHackathon.Web.API.v1.Commands.Wallets;
using PavValHackathon.Web.API.v1.Contracts.Requests;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class EditWalletCommandMapper : IMapperDefinition<EditWalletRequestDocument, EditWalletCommand>
    {
        public EditWalletCommand Map(EditWalletRequestDocument @from)
        {
            throw new System.NotImplementedException();
        }

        public EditWalletCommand Map(EditWalletCommand command, EditWalletRequestDocument document)
        {
            Assert.IsNotNull(command, nameof(command));
            Assert.IsNotNull(document, nameof(document));

            command.Title = document.Title;
            command.CurrencyId = document.CurrencyId;
            
            return command;
        }
    }
}