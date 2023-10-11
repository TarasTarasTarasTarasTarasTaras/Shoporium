using Shoporium.Entities.Enums;

namespace Shoporium.Data._EntityFramework
{
    public class Account
    {
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserType UserType { get; set; }
    }
}
