namespace Shoporium.Data._EntityFramework.Entities
{
    public class RefreshToken
    {
        public long Id { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string TokenString { get; set; } = string.Empty;
        public DateTime ExpirationTimeUtc { get; set; }
        public long? AccountId { get; set; }
        public virtual Account Account { get; set; } = null!;
    }
}
