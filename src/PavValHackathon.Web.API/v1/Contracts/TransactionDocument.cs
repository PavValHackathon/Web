using System;
using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Contracts
{
    [SwaggerDiscriminator("Transaction")]
    public class TransactionDocument
    {
        public int Id { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public decimal Value { get; set; }
        
        public int CurrencyId { get; set; }
        public string Currency { get; set; } = null!;
        
        public int WalletId { get; set; }
        public int BucketId { get; set; }
    }
}