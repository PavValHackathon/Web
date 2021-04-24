using System.Reflection;
using GodelTech.Microservices.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PavValHackathon.Web.Data.Contexts;

namespace PavValHackathon.Web.API.Initializers
{
    public class ServiceDBInitializer : MicroserviceInitializerBase
    {
        public ServiceDBInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("write");

            services.AddDbContext<DataContext>(o => o.UseSqlServer(connectionString,
                sql => sql.MigrationsAssembly(migrationsAssembly)));
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsStaging())
                return;
            
            var applicationServices = app.ApplicationServices;
            var serviceScopeFactory = applicationServices.GetService<IServiceScopeFactory>();
            
            using var scope = serviceScopeFactory!.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            context.Database.Migrate();
            context.SaveChanges();
        }
    }
}