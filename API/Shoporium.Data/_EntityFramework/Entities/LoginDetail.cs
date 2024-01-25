namespace Shoporium.Data._EntityFramework.Entities
{
    public class LoginDetail
    {
        public long LoginDetailId { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime? LastLoginAttemptUtc { get; set; }
        public int FailedLoginAttempts { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
