using System.Net;
using GodelTech.StoryLine;
using GodelTech.StoryLine.Rest.Actions;
using GodelTech.StoryLine.Rest.Expectations;
using GodelTech.StoryLine.Rest.Expectations.Extensions;
using PavValHackathon.Web.IntegrationTests.Actions.Buckets;
using PavValHackathon.Web.IntegrationTests.Actions.Buckets.Contracts;
using PavValHackathon.Web.IntegrationTests.Utils.Security;
using Xunit;

namespace PavValHackathon.Web.IntegrationTests.v1.Buckets
{
    public class GetTests : ApiTestBase
    {
        [Fact]
        public void When_Bucket_Exist_Should_Return_Valid_Report()
        {
            Scenario.New()
                .Given()
                .HasPerformed<CreateBucket>()
                .When()
                .Performs<HttpRequest, BucketDocument>((req, doc) => req
                    .Method("GET")
                    .OAuth2BearerToken(DefaultUserId)
                    .Url($"bucket/{doc.Id}"))
                .Then()
                .Expects<HttpResponse>(res => res
                    .Status(HttpStatusCode.OK)
                    .JsonBody()
                    .Matches()
                    .ResourceFile(new[]
                    {
                        "$.id"
                    }))
                .Run();
        }

        [Fact]
        public void When_Bucket_Not_Exist_Should_Return_NotFound()
        {
            Scenario.New()
                .When()
                .Performs<HttpRequest>(req => req
                    .Method("GET")
                    .Url($"bucket/{int.MaxValue}"))
                .Then()
                .Expects<HttpResponse>(res => res
                    .Status(HttpStatusCode.NotFound));
        }
    }
}