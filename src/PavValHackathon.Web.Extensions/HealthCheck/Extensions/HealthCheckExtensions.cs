using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using PavValHackathon.Web.Extensions.HealthCheck.Factories;

namespace PavValHackathon.Web.Extensions.HealthCheck.Extensions
{
    internal static class HealthCheckExtensions
    {
        public static IApplicationBuilder UseCustomHealthChecks(this IApplicationBuilder applicationBuilder)
        {
            if (applicationBuilder == null) throw new ArgumentNullException(nameof(applicationBuilder));

            return applicationBuilder.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = ResponseWriter
            });
        }

        private static Task ResponseWriter(HttpContext context, HealthReport report)
        {
            var serviceHealthCheckFactory = context.RequestServices.GetService<IServiceHealthReportFactory>();
            var serviceHealthCheck = serviceHealthCheckFactory!.Create(report);

            var jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var jsonResult = JsonConvert.SerializeObject(serviceHealthCheck, Formatting.None, jsonSettings);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            return context.Response.WriteAsync(jsonResult);
        }
    }
}