using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Contracts
{
    [SwaggerDiscriminator("Wallet")]
    public class WalletDocument
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; } = null!;
    }
}