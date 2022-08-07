using PavValHackathon.Web.API.v1.Commands.Transactions;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class TransactionMapper : IMapperDefinition<CreateTransactionCommand, Transaction>
    {
        public Transaction Map(CreateTransactionCommand command)
        {
            Assert.IsNotNull(command, nameof(command));

            return Map(new Transaction(), command);
        }

        public Transaction Map(Transaction domain, CreateTransactionCommand command)
        {
            Assert.IsNotNull(domain, nameof(domain));
            Assert.IsNotNull(command, nameof(command));

            domain.Value = command.Value;
            domain.BucketId = command.BucketId;
            domain.DateTime = command.DateTime;
            domain.WalletId = command.WalletId!.Value;
            domain.CurrencyId = command.CurrencyId;
            
            return domain;
        }
    }
}