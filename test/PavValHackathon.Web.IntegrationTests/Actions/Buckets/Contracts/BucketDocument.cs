namespace PavValHackathon.Web.IntegrationTests.Actions.Buckets.Contracts
{
    public class BucketDocument
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public int? CurrencyId { get; set; }
        public string Currency { get; set; }
        public int? PictureId { get; set; }
    }
}