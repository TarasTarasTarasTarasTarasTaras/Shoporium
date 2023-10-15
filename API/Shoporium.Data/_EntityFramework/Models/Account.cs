using Shoporium.Entities.Enums;

namespace Shoporium.Data._EntityFramework.Models
{
    public class Account
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string MobileNumber { get; set;} = string.Empty;
        public UserType UserType { get; set; }
    }
}
