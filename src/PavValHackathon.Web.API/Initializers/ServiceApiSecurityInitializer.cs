using GodelTech.Microservices.Security;
using Microsoft.Extensions.Configuration;

namespace PavValHackathon.Web.API.Initializers
{
    public class ServiceApiSecurityInitializer : ApiSecurityInitializer
    {
        public ServiceApiSecurityInitializer(IConfiguration configuration) : base(configuration)
        {
        }
    }
}