using GodelTech.StoryLine.Contracts;
using PavValHackathon.Web.IntegrationTests.Actions.Buckets.Contracts;

namespace PavValHackathon.Web.IntegrationTests.Actions.Buckets
{
    public class CreateBucket : IActionBuilder
    {
        private readonly BucketDocument _bucketDocument;
        
        private int _userId = 1;

        public CreateBucket()
        {
            _bucketDocument = CreateDefault();
        }

        public CreateBucket WithCurrencyId(int currencyId)
        {
            _bucketDocument.CurrencyId = currencyId;

            return this;
        }
        
        public CreateBucket WithTitle(string title)
        {
            _bucketDocument.Title = title;

            return this;
        }

        public CreateBucket WithUserId(int userId)
        {
            _userId = userId;

            return this;
        }
        
        public IAction Build()
        {
            return new CreateBucketAction(_bucketDocument, _userId);
        }

        private static BucketDocument CreateDefault()
        {
            return new()
            {
                Title = "Bucket title",
                CurrencyId = 1,
                PictureId = 1
            };
        }
    }
}