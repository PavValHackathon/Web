using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Queries;
using PavValHackathon.Web.Common.Mapping;
using PavValHackathon.Web.Data.Domain;
using PavValHackathon.Web.Data.Repositories;

namespace PavValHackathon.Web.API.v1.Queries.Buckets.Handlers
{
    public class ListBucketQueryHandler : IQueryHandler<ListBucketQuery, PaginationCollection<BucketDocument>>
    {
        private readonly IUserContext _userContext;
        private readonly IReadOnlyRepository<Bucket> _readOnlyRepository;
        private readonly IMapperDefinition<Bucket, BucketDocument> _mapperDefinition;

        public ListBucketQueryHandler(
            IUserContext userContext, 
            IReadOnlyRepository<Bucket> readOnlyRepository,
            IMapperDefinition<Bucket, BucketDocument> mapperDefinition)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _readOnlyRepository = readOnlyRepository ?? throw new ArgumentNullException(nameof(readOnlyRepository));
            _mapperDefinition = mapperDefinition ?? throw new ArgumentNullException(nameof(mapperDefinition));
        }

        public async Task<Result<PaginationCollection<BucketDocument>>> HandleAsync(ListBucketQuery query, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(query, nameof(query));
            cancellationToken.ThrowIfCancellationRequested();

            var userId = _userContext.UserId;
            
            var totalCount = await _readOnlyRepository.CountAsync(p => p.UserId == userId, cancellationToken);
            var buckets = await _readOnlyRepository.ListAsync(p => p.UserId == userId, query.Top, query.Skip, cancellationToken);

            var documents = buckets.Select(_mapperDefinition.Map).ToList();
            var paginationCollection = new PaginationCollection<BucketDocument>()
            {
                Items = documents,
                Count = totalCount
            };
            
            return Result.Ok(paginationCollection);
        } 
    }
}