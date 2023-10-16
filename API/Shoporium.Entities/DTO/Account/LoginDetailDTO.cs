using Shoporium.Entities.Enums;

namespace Shoporium.Entities.DTO.Account
{
    public class LoginDetailDTO
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public DateTime? LastLoginAttemptUtc { get; set; }
        public int FailedLoginAttempts { get; set; }
    }
}
