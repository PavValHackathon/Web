using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs;
using PavValHackathon.Web.Common.Cqrs.Commands;

namespace PavValHackathon.Web.API.Infrastructure.Extensions
{
    public static class RequestHandlerResultExtensions
    {
        public static Result<TResult> InternalError<TRequest, TResult>(this IRequestHandler<TRequest, TResult> handler)
        {
            Assert.IsNotNull(handler, nameof(handler));

            return ResultHelper.InternalError<TResult>();
        }
        
        public static Result<TResult> BadRequest<TRequest, TResult>(this IRequestHandler<TRequest, TResult> handler, string message)
        {
            Assert.IsNotNull(handler, nameof(handler));

            return ResultHelper.BadRequest<TResult>(message);
        }
        
        public static Result<TResult> NotFound<TRequest, TResult>(this IRequestHandler<TRequest, TResult> handler, string message)
        {
            Assert.IsNotNull(handler, nameof(handler));

            return ResultHelper.NotFound<TResult>(message);
        }

        public static Result<Void> Ok<TCommand>(this ICommandHandler<TCommand, Void> handler)
            where TCommand : class, ICommand<Void>
        {
            return Ok(handler, Void.Instance);
        }
        
        public static Result<TResult> Ok<TRequest, TResult>(this IRequestHandler<TRequest, TResult> handler, TResult result)
        {
            Assert.IsNotNull(handler, nameof(handler));
            
            return Result.Ok(result);
        }
    }
}