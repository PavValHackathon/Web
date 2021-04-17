using Microsoft.Extensions.Diagnostics.HealthChecks;
using PavValHackathon.Web.Extensions.HealthCheck.Models;

namespace PavValHackathon.Web.Extensions.HealthCheck.Factories
{
    public interface IServiceHealthInfoFactory
    {
        ServiceHealthInfo Create(string key, HealthReportEntry healthReportEntry);
    }
}