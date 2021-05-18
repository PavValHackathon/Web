using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common.Cqrs.Queries;

namespace PavValHackathon.Web.API.v1.Queries.Buckets
{
    public class GetBucketQuery : IQuery<BucketDocument>
    {
        public GetBucketQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}