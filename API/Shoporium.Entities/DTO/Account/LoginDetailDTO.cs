using Shoporium.Entities.Enums;

namespace Shoporium.Entities.DTO.Account
{
    public class LoginDetailDTO
    {
        public long LoginDetailId { get; set; }
        public long AccountId { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime? LastLoginAttemptUtc { get; set; }
        public int FailedLoginAttempts { get; set; }
    }
}
