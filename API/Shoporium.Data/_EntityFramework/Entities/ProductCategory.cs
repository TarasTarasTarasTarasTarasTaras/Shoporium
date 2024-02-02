namespace Shoporium.Data._EntityFramework.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;

        public int? MainCategoryId { get; set; }
        public virtual ProductCategory? MainCategory { get; set; }
    
    
        public virtual IEnumerable<Product>? Products { get; set; }
        public virtual IEnumerable<ProductCategory>? Subcategories { get; set; }
    }
}
