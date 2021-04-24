using System;
using System.Linq;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PavValHackathon.Web.Extensions.HealthCheck.Models;

namespace PavValHackathon.Web.Extensions.HealthCheck.Factories.Impl
{
    internal class ServiceHealthReportFactory : IServiceHealthReportFactory
    {
        private readonly IServiceHealthInfoFactory _serviceHealthInfoFactory;

        public ServiceHealthReportFactory(IServiceHealthInfoFactory serviceHealthInfoFactory)
        {
            _serviceHealthInfoFactory = serviceHealthInfoFactory ?? throw new ArgumentNullException(nameof(serviceHealthInfoFactory));
        }

        public ServiceHealthReport Create(HealthReport report)
        {
            return new()
            {
                Status = report.Status.ToString(),
                Duration = report.TotalDuration,
                Info = report.Entries.Select(x => _serviceHealthInfoFactory.Create(x.Key, x.Value)).ToArray()
            };
        }	
    }
}