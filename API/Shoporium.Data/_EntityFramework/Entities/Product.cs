using Shoporium.Entities.Enums;

namespace Shoporium.Data._EntityFramework.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public int NumberOfViews { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? CityId { get; set; }
        public ProductCondition Condituon { get; set; }

        public int CategoryId { get; set; }
        public virtual ProductCategory? Category { get; set; }
        
        public long StoreId { get; set; }
        public virtual Store? Store { get; set; }

        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; } = new List<ProductPhoto>();
    }
}
