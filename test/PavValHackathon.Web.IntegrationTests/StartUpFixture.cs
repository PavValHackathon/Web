using System.Reflection;
using GodelTech.StoryLine.Wiremock;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace PavValHackathon.Web.IntegrationTests
{
    public sealed class StartUpFixture
    {
        public StartUpFixture(IMessageSink logger)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .Build();

            GodelTech.StoryLine.Rest.Config.AddServiceEndpont("PavValHackathon.Web", config["ServiceAddress"]);
            GodelTech.StoryLine.Rest.Config.SetAssemblies(typeof(StartUpFixture).GetTypeInfo().Assembly);

            Config.SetBaseAddress(config["WireMockAddress"]);
        }
    }
}