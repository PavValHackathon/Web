using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Contracts.Requests
{
    [SwaggerDiscriminator("EditWalletRequest")]
    public class EditWalletRequestContract
    {
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? Title { get; set; }
        
        [Range(1, Int32.MaxValue)]
        public int? CurrencyId { get; set; }
    }
}