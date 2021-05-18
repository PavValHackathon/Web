using System.Net;
using PavValHackathon.Web.Common;

namespace PavValHackathon.Web.API.Infrastructure
{
    public static class ResultHelper
    {
        private const int InternalSystemErrorCode = (int) HttpStatusCode.InternalServerError;
        private const int BadRequestErrorCode = (int) HttpStatusCode.BadRequest;
        private const int NotFoundErrorCode = (int) HttpStatusCode.NotFound;

        public static Result<TResult> InternalError<TResult>()
            => Result.Failed<TResult>(InternalSystemErrorCode, "Something went wrong. =(");
        
        public static Result<TResult> BadRequest<TResult>(string message)
            => Result.Failed<TResult>(BadRequestErrorCode, message);
        
        public static Result<TResult> NotFound<TResult>(string message)
            => Result.Failed<TResult>(NotFoundErrorCode, message);
    }
}