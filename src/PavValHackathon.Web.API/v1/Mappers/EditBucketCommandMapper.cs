using PavValHackathon.Web.API.v1.Commands.Buckets;
using PavValHackathon.Web.API.v1.Contracts.Requests;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Mapping;

namespace PavValHackathon.Web.API.v1.Mappers
{
    public class EditBucketCommandMapper : IMapperDefinition<EditBucketRequestDocument, EditBucketCommand>
    {
        public EditBucketCommand Map(EditBucketRequestDocument document)
        {
            Assert.IsNotNull(document, nameof(document));

            return Map(new EditBucketCommand(0), document);
        }

        public EditBucketCommand Map(EditBucketCommand command, EditBucketRequestDocument document)
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