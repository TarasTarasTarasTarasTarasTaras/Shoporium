namespace Shoporium.Data._EntityFramework.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public int? MainCategoryId { get; set; }
        public ProductCategory? MainCategory { get; set; }


        public virtual IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        public virtual IEnumerable<ProductCategory> Subcategories { get; set; } = Enumerable.Empty<ProductCategory>();
    }
}
