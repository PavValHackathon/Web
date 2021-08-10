using System.Net;
using GodelTech.StoryLine;
using GodelTech.StoryLine.Rest.Actions;
using GodelTech.StoryLine.Rest.Expectations;
using GodelTech.StoryLine.Rest.Expectations.Extensions;
using Xunit;

namespace PavValHackathon.Web.IntegrationTests.Health
{
    public class GetTests : ApiTestBase
    {
        [Fact] 
        public void When_All_Ok_Should_Return_Healthy()
        {
            Scenario.New()
                .Given()
                .When()
                .Performs<HttpRequest>(req => req
                    .Method("GET")
                    .Url("health"))
                .Then()
                .Expects<HttpResponse>(res => res
                    .Status(HttpStatusCode.OK)
                    .JsonBody()
                    .Matches()
                    .ResourceFile(new[]
                    {
                        "$.Duration",
                        "$.Info[*].Duration"
                    }))
                .Run();
        }
    }
}