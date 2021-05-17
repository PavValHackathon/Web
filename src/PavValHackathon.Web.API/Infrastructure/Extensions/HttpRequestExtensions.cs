using System;
using Microsoft.AspNetCore.Http;
using PavValHackathon.Web.Common;

namespace PavValHackathon.Web.API.Infrastructure.Extensions
{
    public static class HttpRequestExtensions
    {
        public static Uri GetUri(this HttpRequest request)
        {
            Assert.IsNotNull(request, nameof(request));
            
            if (string.IsNullOrWhiteSpace(request.Scheme))
                throw new ArgumentException("Http request Scheme is not specified");
            
            return new Uri(request.Scheme + "://" + (request.Host.HasValue ? request.Host.Value.IndexOf(",", StringComparison.Ordinal) > 0 ? "MULTIPLE-HOST" : request.Host.Value : "UNKNOWN-HOST") + (request.Path.HasValue ? request.Path.Value : string.Empty) + (request.QueryString.HasValue ? request.QueryString.Value : string.Empty));
        }
    }
}