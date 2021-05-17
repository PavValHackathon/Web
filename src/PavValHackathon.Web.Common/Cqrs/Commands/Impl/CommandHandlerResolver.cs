using System;
using Autofac;

namespace PavValHackathon.Web.Common.Cqrs.Commands.Impl
{
    internal  class CommandHandlerResolver : ICommandHandlerResolver
    {
        private readonly ILifetimeScope _lifetimeScope;

        public CommandHandlerResolver(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
        }

        public ICommandHandler<TCommand, TResult> Resolve<TCommand, TResult>() 
            where TCommand : class, ICommand<TResult>
        {
            return _lifetimeScope.Resolve<ICommandHandler<TCommand, TResult>>();
        }
    }
}