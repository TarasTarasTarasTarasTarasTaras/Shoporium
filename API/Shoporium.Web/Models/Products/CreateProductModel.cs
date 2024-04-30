using Shoporium.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shoporium.Web.Models.Products
{
    public class CreateProductModel
    {
        public long UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public IEnumerable<IFormFile> ProductPhotos { get; set; } = Enumerable.Empty<IFormFile>();

        public int? CityId { get; set; }

        [Required]
        public ProductCondition Condituon { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public long StoreId { get; set; }
    }
}
