using Shoporium.Entities.Enums;

namespace Shoporium.Entities.DTO.Account
{
    public class AccountDTO
    {
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public Status Status { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsMobileVerified { get; set; }
    }
}
