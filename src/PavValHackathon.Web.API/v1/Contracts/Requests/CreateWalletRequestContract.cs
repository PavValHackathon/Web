using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Contracts.Requests
{
    [SwaggerDiscriminator("CreateWalletRequest")]
    public class CreateWalletRequestContract
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string Title { get; set; } = null!;
        
        [Required]
        [Range(1, int.MaxValue)]
        public int CurrencyId { get; set; }
    }
}