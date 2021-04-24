using GodelTech.Microservices.Core.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PavValHackathon.Web.API.Filters;

namespace PavValHackathon.Web.API.Initializers
{
    public class ServiceApiInitializer : ApiInitializer
    {
        public ServiceApiInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureMvcOptions(MvcOptions options)
        {
            base.ConfigureMvcOptions(options);

            options.Filters.Add<ResultFilter>();
        }
    }
}