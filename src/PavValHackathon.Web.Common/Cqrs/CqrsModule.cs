using Autofac;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Common.Cqrs.Commands.Impl;
using PavValHackathon.Web.Common.Cqrs.Queries;
using PavValHackathon.Web.Common.Cqrs.Queries.Impl;

namespace PavValHackathon.Web.Common.Cqrs
{
    public abstract class CqrsModule : Module
    {
        protected sealed override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandHandlerResolver>()
                .As<ICommandHandlerResolver>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<CommandExecutor>()
                .As<ICommandExecutor>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<QueryHandlerResolver>()
                .As<IQueryHandlerResolver>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<QueryExecutor>()
                .As<IQueryExecutor>()
                .InstancePerLifetimeScope();
            
            Register(builder);
        }

        protected abstract void Register(ContainerBuilder builder);

        protected static IQueryRegistrationBuilder<TQuery, TResult> RegisterQueryHandler<TQuery, TResult, TQueryHandler>(ContainerBuilder builder)
            where TQuery : class, IQuery<TResult>
            where TQueryHandler : class, IQueryHandler<TQuery, TResult>
        {
            builder.RegisterType<TQueryHandler>()
                .As<IQueryHandler<TQuery, TResult>>()
                .InstancePerLifetimeScope();
            
            return new QueryRegistrationBuilder<TQuery, TResult>(builder);
        }

        protected static ICommandRegistrationBuilder<TCommand> RegisterCommandHandler<TCommand, TCommandHandler>(ContainerBuilder builder)
            where TCommand : class, ICommand
            where TCommandHandler : class, ICommandHandler<TCommand>
        {
            builder.RegisterType<TCommandHandler>()
                .As<ICommandHandler<TCommand>>()
                .InstancePerLifetimeScope();

            return new CommandRegistrationBuilder<TCommand>(builder);
        }
    }
}