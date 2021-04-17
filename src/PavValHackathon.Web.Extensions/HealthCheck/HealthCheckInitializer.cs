using GodelTech.Microservices.Core;
using PavValHackathon.Web.Extensions.HealthCheck.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Extensions.HealthCheck.Factories;
using PavValHackathon.Web.Extensions.HealthCheck.Factories.Impl;

namespace PavValHackathon.Web.Extensions.HealthCheck
{
    public class HealthCheckInitializer : MicroserviceInitializerBase
    {
        public HealthCheckInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            Assert.IsNotNull(services, nameof(services));
            
            services.AddSingleton<IServiceHealthReportFactory, ServiceHealthReportFactory>();
            services.AddSingleton<IServiceHealthInfoFactory, ServiceHealthInfoFactory>();
            
            var builder = services.AddHealthChecks();

            ConfigureHealthChecks(builder);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Assert.IsNotNull(app, nameof(app));
            
            app.UseCustomHealthChecks();
        }

        protected virtual void ConfigureHealthChecks(IHealthChecksBuilder healthChecksBuilder)
        { }
    }
}