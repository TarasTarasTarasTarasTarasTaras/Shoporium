namespace Shoporium.Data._EntityFramework.Entities
{
    public class LoginDetail
    {
        public long LoginDetailId { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime? LastLoginAttemptUtc { get; set; }
        public int FailedLoginAttempts { get; set; }
        public long AccountId { get; set; }
        public virtual Account Account { get; set; } = null!;
    }
}
