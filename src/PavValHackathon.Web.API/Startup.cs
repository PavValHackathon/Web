using System.Collections.Generic;
using Autofac;
using GodelTech.Microservices.Core;
using GodelTech.Microservices.Core.Mvc;
using GodelTech.Microservices.SharedServices;
using GodelTech.Microservices.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PavValHackathon.Web.API.Initializers;
using PavValHackathon.Web.API.Modules;

namespace PavValHackathon.Web.API
{
    public class Startup : MicroserviceStartup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<CoreMappingModule>();
            builder.RegisterModule<CoreCqrsModule>();
            builder.RegisterModule<CoreDbModule>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            base.ConfigureServices(services);
        }

        protected override IEnumerable<IMicroserviceInitializer> CreateInitializers()
        {
            yield return new DeveloperExceptionPageInitializer(Configuration);
            yield return new HttpsInitializer(Configuration);

            yield return new GenericInitializer((app, env) => app.UseRouting());
            
            yield return new SharedServicesInitializer(Configuration);
            yield return new SwaggerInitializer(Configuration);
            yield return new ServiceApiSecurityInitializer(Configuration);
            yield return new ServiceApiInitializer(Configuration);
            yield return new ServiceDBInitializer(Configuration);

            yield return new ServiceHealthCheckInitializer(Configuration);
        }
    }
}