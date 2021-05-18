using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Contracts.Requests
{
    [SwaggerDiscriminator("CreateBucketRequest")]
    public class CreateBucketRequestDocument
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; } = null!;
        
        [Required]
        [Range(1, int.MaxValue)]
        public int CurrencyId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int PictureId { get; set; }
    }
}