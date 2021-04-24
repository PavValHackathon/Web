using System;
using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Queries
{
    public abstract class QueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> 
        where TQuery : class, IQuery<TResult>
    {
        protected IQueryHandler<TQuery, TResult> InnerHandler { get; }

        protected QueryHandlerDecorator(IQueryHandler<TQuery, TResult> innerHandler)
        {
            InnerHandler = innerHandler ?? throw new ArgumentNullException(nameof(innerHandler));
        }

        public abstract Task<Result<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}