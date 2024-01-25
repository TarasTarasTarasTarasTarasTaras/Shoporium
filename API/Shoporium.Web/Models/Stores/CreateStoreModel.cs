using Shoporium.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shoporium.Web.Models.Stores
{
    public class CreateStoreModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MainPhoto { get; set; } = string.Empty;
        public string BackgroundPhoto { get; set; } = string.Empty;
        public string OtherCategoryName { get; set; } = string.Empty;
        public StoreStatus Status { get; set; }

        public long UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
