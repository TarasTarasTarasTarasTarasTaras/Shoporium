using Shoporium.Entities.Enums;

namespace Shoporium.Data._EntityFramework.Entities
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MainPhoto { get; set; } = string.Empty;
        public string BackgroundPhoto { get; set; } = string.Empty;
        public string OtherCategoryName { get; set; } = string.Empty;
        public StoreStatus Status { get; set; }

        public long UserId { get; set; }
        public virtual User? User { get; set; }

        public int CategoryId { get; set; }
        public virtual StoreCategory? Category { get; set; }

        public virtual IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
    }
}
