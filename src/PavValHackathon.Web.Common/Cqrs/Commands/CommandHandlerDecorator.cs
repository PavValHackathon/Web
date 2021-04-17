using System;
using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Commands
{
    public abstract class CommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> 
        where TCommand : class, ICommand
    {
        protected ICommandHandler<TCommand> InnerHandler { get; }
        
        protected CommandHandlerDecorator(ICommandHandler<TCommand> innerHandler)
        {
            InnerHandler = innerHandler ?? throw new ArgumentNullException(nameof(innerHandler));
        }

        public abstract Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}