using System.ComponentModel.DataAnnotations;

namespace Shoporium.Web.Models.User
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string MobileNumber { get; set; } = string.Empty;
    }
}
