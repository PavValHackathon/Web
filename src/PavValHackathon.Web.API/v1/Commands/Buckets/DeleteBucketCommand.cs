using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;

namespace PavValHackathon.Web.API.v1.Commands.Buckets
{
    public class DeleteBucketCommand : ICommand<Void>
    {
        public DeleteBucketCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}