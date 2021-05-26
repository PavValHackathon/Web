using System.Net;
using GodelTech.StoryLine;
using GodelTech.StoryLine.Rest.Actions;
using GodelTech.StoryLine.Rest.Expectations;
using GodelTech.StoryLine.Rest.Expectations.Extensions;
using PavValHackathon.Web.IntegrationTests.Actions.Buckets;
using PavValHackathon.Web.IntegrationTests.Utils.Security;
using Xunit;

namespace PavValHackathon.Web.IntegrationTests.v1.Buckets
{
    public class GetAllTests : ApiTestBase
    {
        [Fact]
        public void When_All_Ok_Should_Return_Buckets_Collection()
        {
            const int UserId = 999;
            
            Scenario.New()
                .Given()
                .HasPerformed<CreateBucket>(builder => builder
                    .WithTitle(nameof(When_All_Ok_Should_Return_Buckets_Collection))
                    .WithUserId(UserId))
                .When()
                .Performs<HttpRequest>(req => req
                    .Method("GET")
                    .OAuth2BearerToken(UserId)
                    .Url("bucket"))
                .Then()
                .Expects<HttpResponse>(res => res
                    .Status(HttpStatusCode.OK)
                    .JsonBody()
                    .Matches()
                    .ResourceFile(new[]
                    {
                        "$.items[*].id"
                    }))
                .Run();

        }
    }
}