namespace Shoporium.Data._EntityFramework.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NumberOfViews { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public int CategoryId { get; set; }
        public virtual ProductCategory? Category { get; set; }
        
        public long AccountId { get; set; }
        public virtual Account? Account { get; set; }
    }
}
