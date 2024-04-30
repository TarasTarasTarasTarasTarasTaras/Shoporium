using Shoporium.Entities.Enums;

namespace Shoporium.Entities.DTO.Products
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public int NumberOfViews { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<string> ProductPhotos { get; set; } = Enumerable.Empty<string>();
        public List<byte[]?> DownloadedPhotos { get; set; } = new();

        public int? CityId { get; set; }
        public ProductCondition Condituon { get; set; }

        public int CategoryId { get; set; }
        public long StoreId { get; set; }

        //public StoreDTO? Store { get; set; }
    }
}
