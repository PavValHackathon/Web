using System;
using System.Threading;
using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Cqrs.Commands
{
    public abstract class CommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult> 
        where TCommand : class, ICommand<TResult>
    {
        protected ICommandHandler<TCommand, TResult> InnerHandler { get; }
        
        protected CommandHandlerDecorator(ICommandHandler<TCommand, TResult> innerHandler)
        {
            InnerHandler = innerHandler ?? throw new ArgumentNullException(nameof(innerHandler));
        }

        public abstract Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}