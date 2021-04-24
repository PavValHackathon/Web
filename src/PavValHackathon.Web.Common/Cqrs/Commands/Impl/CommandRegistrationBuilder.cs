using System;
using Autofac;
using PavValHackathon.Web.Common.Cqrs.Commands.Decorators;

namespace PavValHackathon.Web.Common.Cqrs.Commands.Impl
{
    internal class CommandRegistrationBuilder<TCommand> : ICommandRegistrationBuilder<TCommand> 
        where TCommand : class, ICommand
    {
        private readonly ContainerBuilder _containerBuilder;

        public CommandRegistrationBuilder(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder ?? throw new ArgumentNullException(nameof(containerBuilder));
        }

        public ICommandRegistrationBuilder<TCommand> RegisterDecorator<TDecorator>() 
            where TDecorator : CommandHandlerDecorator<TCommand>
        {
            _containerBuilder.RegisterDecorator<TDecorator, ICommandHandler<TCommand>>();

            return this;
        }

        public void InstancePerLifetimeScope()
        {
            _containerBuilder.RegisterDecorator<ExceptionCatcherCommandDecorator<TCommand>, ICommandHandler<TCommand>>();
            _containerBuilder.RegisterDecorator<LoggerCommandDecorator<TCommand>, ICommandHandler<TCommand>>();
        }
    }
}