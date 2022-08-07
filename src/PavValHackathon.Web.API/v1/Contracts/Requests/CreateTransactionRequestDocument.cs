using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Contracts.Requests
{
    [SwaggerDiscriminator("CreateTransactionRequest")]
    public class CreateTransactionRequestDocument
    {
        [Required]
        public decimal Value { get; set; }
        
        [Required]
        public DateTime DateTime { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int CurrencyId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int BucketId { get; set; }
        
        [Range(1, int.MaxValue)]
        public int? WalletId { get; set; }
    }
}