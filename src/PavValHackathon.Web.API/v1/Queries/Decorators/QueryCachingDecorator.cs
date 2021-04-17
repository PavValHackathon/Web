using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Queries;

namespace PavValHackathon.Web.API.v1.Queries.Decorators
{
    public class QueryCachingDecorator<TQuery, TResult> : QueryHandlerDecorator<TQuery, TResult> 
        where TQuery : class, IQuery<TResult>
    {
        private readonly ILogger<QueryCachingDecorator<TQuery, TResult>> _logger;
        private readonly IMemoryCache _memoryCache;
        
        public QueryCachingDecorator(
            IQueryHandler<TQuery, TResult> innerHandler,
            IMemoryCache memoryCache, 
            ILogger<QueryCachingDecorator<TQuery, TResult>> logger) 
            : base(innerHandler)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<Result<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(query, nameof(query));
            cancellationToken.ThrowIfCancellationRequested();

            if (_memoryCache.TryGetValue<TResult>(typeof(TQuery).Name, out var value))
                return Result.Ok(value);

            _logger.LogInformation("Action='{Query}' Message='Cache miss.'", typeof(TQuery).Name);
            
            var queryResult = await InnerHandler.HandleAsync(query, cancellationToken);

            if (!queryResult.IsFailed)
                _memoryCache.Set(typeof(TQuery).Name, queryResult.Value);

            return queryResult;
        }
    }
}