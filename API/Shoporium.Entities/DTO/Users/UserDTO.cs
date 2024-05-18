using Shoporium.Entities.Enums;

namespace Shoporium.Entities.DTO.Users
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public GeneralStatus Status { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsMobileVerified { get; set; }
    }
}
