using System.Collections.Generic;
using System.Net;
using GodelTech.StoryLine;
using GodelTech.StoryLine.Rest.Actions;
using GodelTech.StoryLine.Rest.Actions.Extensions;
using GodelTech.StoryLine.Rest.Expectations;
using PavValHackathon.Web.IntegrationTests.Actions.Buckets.Contracts;
using PavValHackathon.Web.IntegrationTests.Utils.Security;
using Xunit;

namespace PavValHackathon.Web.IntegrationTests.v1.Buckets
{
    public class PostTests : ApiTestBase
    {
        [Theory]
        [MemberData(nameof(InvalidDocumentsCollection))]
        public void When_BucketDocument_Invalid_Exist_Should_Return_BadRequest(BucketDocument document)
        {
            
            Scenario.New()
                .When()
                .Performs<HttpRequest>(req => req
                    .Url("bucket")
                    .Method("POST")
                    .Header("Content-Type", "application/json")
                    .OAuth2BearerToken(DefaultUserId)
                    .JsonObjectBody(document))
                .Then()
                .Expects<HttpResponse>(res => res
                    .Status(HttpStatusCode.BadRequest))
                .Run();
        }

        public static IEnumerable<object[]> InvalidDocumentsCollection()
        {
            return new[]
            {
                new[] { new BucketDocument { Title = "title", CurrencyId = 1 } },
                new[] { new BucketDocument { Title = "title", PictureId = 1 } },
                new[] { new BucketDocument { CurrencyId = 1, PictureId = 1 } },
                new[] { new BucketDocument() },
                new[] { new BucketDocument { Title = "title", CurrencyId = 1, PictureId = -1 } },
                new[] { new BucketDocument { Title = "title", CurrencyId = -1, PictureId = 1 } },
                new[] { new BucketDocument { Title = "", CurrencyId = 1, PictureId = 1 } }
            };
        }
    }
}