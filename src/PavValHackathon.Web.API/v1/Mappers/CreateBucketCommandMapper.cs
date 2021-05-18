using PavValHackathon.Web.API.v1.Commands.Buckets;
using PavValHackathon.Web.API.v1.Contracts.Requests;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class CreateBucketCommandMapper : IMapperDefinition<CreateBucketRequestDocument, CreateBucketCommand>
    {
        public CreateBucketCommand Map(CreateBucketRequestDocument document)
        {
            Assert.IsNotNull(document, nameof(document));

            return Map(new CreateBucketCommand(), document);
        }

        public CreateBucketCommand Map(CreateBucketCommand command, CreateBucketRequestDocument document)
        {
            Assert.IsNotNull(command, nameof(command));
            Assert.IsNotNull(document, nameof(document));

            command.Title = document.Title;
            command.PictureId = document.PictureId;
            command.CurrencyId = document.CurrencyId;
            
            return command;
        }
    }
}