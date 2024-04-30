namespace Shoporium.Data._EntityFramework.Entities
{
    public class ProductPhoto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public long ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
