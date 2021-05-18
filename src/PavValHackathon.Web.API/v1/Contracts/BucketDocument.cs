using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Contracts
{
    [SwaggerDiscriminator("Bucket")]
    public class BucketDocument
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; } = null!;
        public int PictureId { get; set; }
    }
}