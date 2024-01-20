namespace Shoporium.Data._EntityFramework.Entities
{
    public class Token
    {
        public long TokenId { get; set; }
        public string TokenValue { get; set; } = string.Empty;
        public byte ActionType { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
        public DateTime CreatedOn { get; set; }
        public long AccountId { get; set; }
        public virtual Account Account { get; set; } = null!;
    }
}
