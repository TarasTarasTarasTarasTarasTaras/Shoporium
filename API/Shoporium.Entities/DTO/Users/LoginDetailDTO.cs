namespace Shoporium.Entities.DTO.Users
{
    public class LoginDetailDTO
    {
        public long LoginDetailId { get; set; }
        public long UserId { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime? LastLoginAttemptUtc { get; set; }
        public int FailedLoginAttempts { get; set; }
    }
}
