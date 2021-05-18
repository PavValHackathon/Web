using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class BucketDocumentMapper : IMapperDefinition<Bucket, BucketDocument>
    {
        public BucketDocument Map(Bucket domain)
        {
            Assert.IsNotNull(domain, nameof(domain));

            return Map(new BucketDocument(), domain);
        }

        public BucketDocument Map(BucketDocument document, Bucket domain)
        {
            Assert.IsNotNull(document, nameof(document));
            Assert.IsNotNull(domain, nameof(domain));

            document.Id = domain.Id;
            document.Title = domain.Title;
            document.Amount = domain.Amount;
            document.PictureId = domain.PictureId;

            document.Currency = domain.Currency.Name;
            document.CurrencyId = domain.Currency.Id;
            
            return document;
        }
    }
}