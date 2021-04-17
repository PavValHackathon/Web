using System;
using Autofac;

namespace PavValHackathon.Web.Common.Cqrs.Queries.Impl
{
    internal class QueryHandlerResolver : IQueryHandlerResolver
    {
        private readonly ILifetimeScope _lifetimeScope;

        public QueryHandlerResolver(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
        }

        public IQueryHandler<TQuery, TResult> Resolve<TQuery, TResult>()
            where TQuery : class, IQuery<TResult>
        {
            return _lifetimeScope.Resolve<IQueryHandler<TQuery, TResult>>();
        }
    }
}