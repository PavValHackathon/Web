using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common.Cqrs.Queries;

namespace PavValHackathon.Web.API.v1.Queries.Buckets
{
    public class ListBucketQuery : PaginationFilter, IQuery<PaginationCollection<BucketDocument>>
    {
    }
}