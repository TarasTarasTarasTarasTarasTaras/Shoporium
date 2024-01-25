namespace Shoporium.Data._EntityFramework.Entities
{
    public class StoreCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual IEnumerable<Store> Stores { get; set; } = Enumerable.Empty<Store>();
    }
}
