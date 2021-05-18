using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using PavValHackathon.Web.Common;

namespace PavValHackathon.Web.API.Filters
{   
    public class ResultFilter : IAsyncActionFilter 
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext executingContext, ActionExecutionDelegate next)
        {
            var context = await next();
            
            if (context.ExceptionHandled)
                return;

            if (context.Result is not ObjectResult objectResult ||
                objectResult.Value is not Result result)
                return;

            if (result.IsFailed is false)
            {
                objectResult.Value = result is ValueResult valueResult
                    ? valueResult.Value
                    : null;
                
                return;
            }
                
            
            var serviceProvider = context.HttpContext.RequestServices;
            var problemDetailsFactory = (ProblemDetailsFactory)serviceProvider.GetService(typeof(ProblemDetailsFactory))!;
            var problemDetails = problemDetailsFactory.CreateProblemDetails(context.HttpContext);

            problemDetails.Status = result.ErrorCode;
            problemDetails.Detail = result.ErrorMessage;
            problemDetails.Extensions["traceId"] = result.TraceId;

            context.HttpContext.Response.StatusCode = result.ErrorCode!.Value;
            context.ExceptionHandled = true;
            context.Result = new ObjectResult(problemDetails);
            
            var logger = (ILogger<ResultFilter>)serviceProvider.GetService(typeof(ILogger<ResultFilter>))!;
            logger.LogError("TrackId='{TraceId}' Request failed with message: {ErrorMessage}", result.TraceId, result.ErrorMessage);
        }
    }
}