using PavValHackathon.Web.Common.Cqrs.Commands;

namespace PavValHackathon.Web.API.v1.Commands.Buckets
{
    public class CreateBucketCommand : ICommand<int>
    {
        public string Title { get; set; } = null!;
        public int CurrencyId { get; set; }
        public int PictureId { get; set; }
    }
}