using Shoporium.Entities.Enums;

namespace Shoporium.Data._EntityFramework.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set;} = string.Empty;
        public UserType UserType { get; set; }
        public GeneralStatus Status { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsMobileVerified { get; set; }

        public int? InnerCityId { get; set; }
        public virtual InnerCity? InnerCity { get; set; }

        public virtual LoginDetail? LoginDetail { get; set; }
        public virtual IEnumerable<Store>? Stores { get; set; } = Enumerable.Empty<Store>();
    }
}
