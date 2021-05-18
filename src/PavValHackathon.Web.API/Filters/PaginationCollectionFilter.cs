using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.Infrastructure.Extensions;

namespace PavValHackathon.Web.API.Filters
{
    public class PaginationCollectionFilter : IAsyncResultFilter
    {
        private static readonly PaginationFilter Default = new();

        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.HttpContext.Response.StatusCode != (int) HttpStatusCode.OK)
                return next.Invoke();

            var objectResult = context.Result as OkObjectResult;

            if (objectResult?.Value is not PaginationCollection paginationCollection)
                return next.Invoke();

            var uri = context.HttpContext.Request.GetUri();
            var path = uri.AbsoluteUri.Split('?')[0];
            var filter = GetPaginationFilter(context.HttpContext);

            if (paginationCollection.Count > filter.Top)
            {
                paginationCollection.SetPrevHref(path, filter);
                paginationCollection.SetNexHref(path, filter);
            }

            paginationCollection.SetFirstHref(path, filter);
            paginationCollection.SetLastHref(path, filter);

            return next.Invoke();
        }

        private static PaginationFilter GetPaginationFilter(HttpContext context)
        {
            var skipString = context.Request.Query[nameof(PaginationFilter.Skip)];
            var topString = context.Request.Query[nameof(PaginationFilter.Top)];

            return new PaginationFilter
            {
                Skip = skipString.Count == 0 ? Default.Skip : int.Parse(skipString),
                Top = topString.Count == 0 ? Default.Top : int.Parse(topString)
            };
        }
    }
}