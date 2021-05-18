using PavValHackathon.Web.API.v1.Commands.Buckets;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class BucketMapper : IMapperDefinition<CreateBucketCommand, Bucket>
    {
        public Bucket Map(CreateBucketCommand command)
        {
            Assert.IsNotNull(command, nameof(command));

            return Map(new Bucket(), command);
        }

        public Bucket Map(Bucket domain, CreateBucketCommand command)
        {
            Assert.IsNotNull(domain, nameof(domain));
            Assert.IsNotNull(command, nameof(command));

            domain.Title = command.Title;
            domain.PictureId = command.PictureId;
            domain.CurrencyId = command.CurrencyId;
            
            return domain;
        }
    }
}