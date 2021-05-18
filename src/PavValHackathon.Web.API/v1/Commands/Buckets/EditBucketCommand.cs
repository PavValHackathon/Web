using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;

namespace PavValHackathon.Web.API.v1.Commands.Buckets
{
    public class EditBucketCommand : ICommand<Void>
    {
        public EditBucketCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public string? Title { get; set; }
        public int? PictureId { get; set; }
        public int? CurrencyId { get; set; }
    }
}