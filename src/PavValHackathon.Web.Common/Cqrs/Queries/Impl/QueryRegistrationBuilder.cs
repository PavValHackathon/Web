using System;
using Autofac;
using PavValHackathon.Web.Common.Cqrs.Queries.Decorators;

namespace PavValHackathon.Web.Common.Cqrs.Queries.Impl
{
    internal class QueryRegistrationBuilder<TQuery, TResult> : IQueryRegistrationBuilder<TQuery, TResult> 
        where TQuery : class, IQuery<TResult>
    {
        private readonly ContainerBuilder _builder;
        
        public QueryRegistrationBuilder(ContainerBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
        
        public IQueryRegistrationBuilder<TQuery, TResult> RegisterDecorator<TDecorator>() 
            where TDecorator : QueryHandlerDecorator<TQuery, TResult>
        {
            _builder.RegisterDecorator<TDecorator, IQueryHandler<TQuery, TResult>>();

            return this;
        }

        public void InstancePerLifetimeScope()
        {
            _builder.RegisterDecorator<ExceptionCatcherQueryDecorator<TQuery, TResult>, IQueryHandler<TQuery, TResult>>();
            _builder.RegisterDecorator<LoggerQueryDecorator<TQuery, TResult>, IQueryHandler<TQuery, TResult>>();
        }
    }
}