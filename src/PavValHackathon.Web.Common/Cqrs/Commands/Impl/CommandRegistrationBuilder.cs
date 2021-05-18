using System;
using Autofac;
using PavValHackathon.Web.Common.Cqrs.Commands.Decorators;

namespace PavValHackathon.Web.Common.Cqrs.Commands.Impl
{
    internal class CommandRegistrationBuilder<TCommand, TResult> : ICommandRegistrationBuilder<TCommand, TResult> 
        where TCommand : class, ICommand<TResult>
    {
        private readonly ContainerBuilder _containerBuilder;

        public CommandRegistrationBuilder(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder ?? throw new ArgumentNullException(nameof(containerBuilder));
        }

        public ICommandRegistrationBuilder<TCommand, TResult> RegisterDecorator<TDecorator>() 
            where TDecorator : CommandHandlerDecorator<TCommand, TResult>
        {
            _containerBuilder.RegisterDecorator<TDecorator, ICommandHandler<TCommand, TResult>>();

            return this;
        }

        public void InstancePerLifetimeScope()
        {
            _containerBuilder.RegisterDecorator<ExceptionCatcherCommandDecorator<TCommand, TResult>, ICommandHandler<TCommand, TResult>>();
            _containerBuilder.RegisterDecorator<LoggerCommandDecorator<TCommand, TResult>, ICommandHandler<TCommand, TResult>>();
        }
    }
}