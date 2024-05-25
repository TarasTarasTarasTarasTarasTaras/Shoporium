namespace Shoporium.Data._EntityFramework.Entities
{
    public class InnerCity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public int RegionId { get; set; }
        public virtual City? Region { get; set; }
    }
}
