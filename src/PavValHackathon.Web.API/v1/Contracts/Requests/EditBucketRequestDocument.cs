using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Contracts.Requests
{
    [SwaggerDiscriminator("DeleteBucketRequest")]
    public class EditBucketRequestDocument
    {   
        [MinLength(1)]
        public string? Title { get; set; }
        
        [Range(1, int.MaxValue)]
        public int? PictureId { get; set; }
        
        [Range(1, int.MaxValue)]
        public int? CurrencyId { get; set; }
    }
}