using System;
using System.Net;
using GodelTech.StoryLine;
using GodelTech.StoryLine.Contracts;
using GodelTech.StoryLine.Rest.Actions;
using GodelTech.StoryLine.Rest.Actions.Extensions;
using GodelTech.StoryLine.Rest.Expectations;
using GodelTech.StoryLine.Rest.Extensions;
using GodelTech.StoryLine.Rest.Services.Http;
using GodelTech.StoryLine.Utils.Actions;
using GodelTech.StoryLine.Utils.Services;
using PavValHackathon.Web.IntegrationTests.Actions.Buckets.Contracts;
using PavValHackathon.Web.IntegrationTests.Utils.Security;

namespace PavValHackathon.Web.IntegrationTests.Actions.Buckets
{
    public class CreateBucketAction : IAction
    {
        private readonly BucketDocument _bucketDocument;

        private int _userId;

        public CreateBucketAction(BucketDocument bucketDocument, int userId)
        {
            _bucketDocument = bucketDocument ?? throw new ArgumentNullException(nameof(bucketDocument));
            _userId = userId;
        }
        
        public void Execute(IActor actor)
        {
            Scenario.New()
                .Given(actor)
                .When(actor)
                .Performs<HttpRequest>(req => req
                    .Method("POST")
                    .Url("bucket")
                    .Header("Content-Type", "application/json")
                    .OAuth2BearerToken(_userId)
                    .JsonObjectBody(_bucketDocument))
                .Performs<Transform, IResponse>(ConfigureTransform)
                .Then(actor)
                .Expects<HttpResponse>(res => res.Status(HttpStatusCode.Created))
                .Run();
        }

        private static void ConfigureTransform(Transform transform, IResponse response)
        {
            transform
                .From(response.GetText())
                .To<BucketDocument>()
                .Using<JsonConverter>();
        }
    }
}