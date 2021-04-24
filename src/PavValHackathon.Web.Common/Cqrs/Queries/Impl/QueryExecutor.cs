using System;
using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Queries.Impl
{
    internal class QueryExecutor : IQueryExecutor
    {
        private readonly IQueryHandlerResolver _queryHandlerResolver;

        public QueryExecutor(IQueryHandlerResolver queryHandlerResolver)
        {
            _queryHandlerResolver = queryHandlerResolver ?? throw new ArgumentNullException(nameof(queryHandlerResolver));
        }

        public Task<Result<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) 
            where TQuery : class, IQuery<TResult>
        {
            Assert.IsNotNull(query, nameof(query));
            cancellationToken.ThrowIfCancellationRequested();

            var handler = _queryHandlerResolver.Resolve<TQuery, TResult>();
            return handler.HandleAsync(query, cancellationToken);
        }
    }
}