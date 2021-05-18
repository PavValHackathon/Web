using System;
using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Commands.Impl
{
    internal class CommandExecutor : ICommandExecutor
    {
        private readonly ICommandHandlerResolver _commandHandlerResolver;

        public CommandExecutor(ICommandHandlerResolver commandHandlerResolver)
        {
            _commandHandlerResolver = commandHandlerResolver ?? throw new ArgumentNullException(nameof(commandHandlerResolver));
        }

        public Task<Result<TResult>> ExecuteAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) 
            where TCommand : class, ICommand<TResult>
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            var handler = _commandHandlerResolver.Resolve<TCommand, TResult>();
            return handler.HandleAsync(command, cancellationToken);
        }
    }
}