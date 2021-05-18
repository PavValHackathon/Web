using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;

namespace PavValHackathon.Web.API.Infrastructure.Extensions
{
    public static class CommandHandlerResultExtensions
    {
        public static Result<TResult> InternalError<TCommand, TResult>(this ICommandHandler<TCommand, TResult> handler)
            where TCommand : class, ICommand<TResult>
        {
            Assert.IsNotNull(handler, nameof(handler));

            return ResultHelper.InternalError<TResult>();
        }
        
        public static Result<TResult> BadRequest<TCommand, TResult>(this ICommandHandler<TCommand, TResult> handler, string message)
            where TCommand : class, ICommand<TResult>
        {
            Assert.IsNotNull(handler, nameof(handler));

            return ResultHelper.BadRequest<TResult>(message);
        }
        
        public static Result<TResult> NotFound<TCommand, TResult>(this ICommandHandler<TCommand, TResult> handler, string message)
            where TCommand : class, ICommand<TResult>
        {
            Assert.IsNotNull(handler, nameof(handler));

            return ResultHelper.NotFound<TResult>(message);
        }

        public static Result<Void> Ok<TCommand>(this ICommandHandler<TCommand, Void> handler)
            where TCommand : class, ICommand<Void>
        {
            return Ok(handler, Void.Instance);
        }
        
        public static Result<TResult> Ok<TCommand, TResult>(this ICommandHandler<TCommand, TResult> handler, TResult result)
            where TCommand : class, ICommand<TResult>
        {
            Assert.IsNotNull(handler, nameof(handler));
            
            return Result.Ok(result);
        }
    }
}