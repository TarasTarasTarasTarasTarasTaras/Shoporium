using Shoporium.Data._EntityFramework.Entities;
using Shoporium.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shoporium.Web.Models.Stores
{
    public class CreateStoreModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string OtherCategoryName { get; set; } = string.Empty;

        public StoreStatus Status { get; set; }

        public IFormFile? MainPhoto { get; set; }
        public IFormFile? BackgroundPhoto { get; set; }

        public long UserId { get; set; }
    }
}
