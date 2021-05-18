using System;
using System.Net;
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
    public class GetBucketQueryHandler : IQueryHandler<GetBucketQuery, BucketDocument>
    {
        private readonly IUserContext _userContext;
        private readonly IReadOnlyRepository<Bucket> _readOnlyRepository;
        private readonly IMapperDefinition<Bucket, BucketDocument> _mapperDefinition;

        public GetBucketQueryHandler(
            IUserContext userContext, 
            IReadOnlyRepository<Bucket> readOnlyRepository,
            IMapperDefinition<Bucket, BucketDocument> mapperDefinition)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _readOnlyRepository = readOnlyRepository ?? throw new ArgumentNullException(nameof(readOnlyRepository));
            _mapperDefinition = mapperDefinition ?? throw new ArgumentNullException(nameof(mapperDefinition));
        }

        public async Task<Result<BucketDocument>> HandleAsync(GetBucketQuery query, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(query, nameof(query));
            cancellationToken.ThrowIfCancellationRequested();

            var userId = _userContext.UserId;
            var bucket = await _readOnlyRepository.GetAsync(p => p.Id == query.Id && p.UserId == userId, cancellationToken);

            if (bucket is null)
                return Result.Failed<BucketDocument>((int) HttpStatusCode.NotFound, "Bucket not found.");

            var document = _mapperDefinition.Map(bucket); 
            
            return Result.Ok(document);
        }
    }
}