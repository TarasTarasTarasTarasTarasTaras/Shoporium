using Shoporium.Entities.Enums;

namespace Shoporium.Entities.DTO.Users
{
    public class RefreshTokenDTO
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string TokenString { get; set; } = string.Empty;
        public DateTime ExpirationTimeUtc { get; set; }

        public UserType? Type
        {
            get
            {
                if (UserId > 0) return UserType.User;

                return null;
            }
        }

        public long? ActualId
        {
            get
            {
                if (UserId > 0) return UserId;

                return null;
            }
        }
    }
}
