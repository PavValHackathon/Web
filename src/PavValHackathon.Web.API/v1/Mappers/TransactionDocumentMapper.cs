using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class TransactionDocumentMapper : IMapperDefinition<Transaction, TransactionDocument>
    {
        public TransactionDocument Map(Transaction domain)
        {
            Assert.IsNotNull(domain, nameof(domain));

            return Map(new TransactionDocument(), domain);
        }

        public TransactionDocument Map(TransactionDocument document, Transaction domain)
        {
            Assert.IsNotNull(document, nameof(document));
            Assert.IsNotNull(domain, nameof(domain));
            Assert.IsNotNull(domain.Currency, nameof(document.Currency));

            document.Id = domain.Id;
            document.Value = domain.Value;
            document.DateTime = domain.DateTime;
            document.BucketId = domain.BucketId;
            document.WalletId = domain.WalletId;

            document.CurrencyId = domain.Currency.Id;
            document.Currency = domain.Currency.Name;
            
            return document;
        }
    }
}