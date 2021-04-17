using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Extensions.HealthCheck.Models;

namespace PavValHackathon.Web.Extensions.HealthCheck.Factories.Impl
{
    internal class ServiceHealthInfoFactory : IServiceHealthInfoFactory
    {
        public ServiceHealthInfo Create(string key, HealthReportEntry healthReportEntry)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            return new ServiceHealthInfo
            {
                Key = key,
                Description = healthReportEntry.Description ?? string.Empty,
                Duration = healthReportEntry.Duration,
                Status = healthReportEntry.Status.ToString(),
                Error = healthReportEntry.Exception?.Message
            };
        }
    }
}