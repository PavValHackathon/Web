using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PavValHackathon.Web.Extensions.HealthCheck;
using PavValHackathon.Web.Extensions.HealthCheck.Checks;

namespace PavValHackathon.Web.API.Initializers
{
    public class ServiceHealthCheckInitializer : HealthCheckInitializer
    {
        public ServiceHealthCheckInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureHealthChecks(IHealthChecksBuilder healthChecksBuilder)
        {
            var connectionString = Configuration.GetConnectionString("write");
            
            healthChecksBuilder.AddCheck(nameof(SqlServerHealthCheck), new SqlServerHealthCheck(connectionString));
        }
    }
}