using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Contracts.Requests
{
    [SwaggerDiscriminator("EditWalletRequest")]
    public class EditWalletRequestDocument
    {
        [MinLength(1)]
        public string? Title { get; set; }
        
        [Range(1, int.MaxValue)]
        public int? CurrencyId { get; set; }
    }
}