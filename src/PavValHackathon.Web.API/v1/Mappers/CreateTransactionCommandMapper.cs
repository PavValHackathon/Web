using PavValHackathon.Web.API.v1.Commands.Transactions;
using PavValHackathon.Web.API.v1.Contracts.Requests;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class CreateTransactionCommandMapper : IMapperDefinition<CreateTransactionRequestDocument, CreateTransactionCommand>
    {
        public CreateTransactionCommand Map(CreateTransactionRequestDocument document)
        {
            Assert.IsNotNull(document, nameof(document));

            return Map(new CreateTransactionCommand(), document);
        }

        public CreateTransactionCommand Map(CreateTransactionCommand command, CreateTransactionRequestDocument document)
        {
            Assert.IsNotNull(command, nameof(command));
            Assert.IsNotNull(document, nameof(document));

            command.Value = document.Value;
            command.DateTime = document.DateTime;
            command.WalletId = document.WalletId;
            command.BucketId = document.BucketId;
            command.CurrencyId = document.CurrencyId;
            
            return command;
        }
    }
}