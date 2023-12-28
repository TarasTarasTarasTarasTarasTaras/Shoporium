using Shoporium.Entities.Enums;

namespace Shoporium.Entities.DTO.Account
{
    public class RefreshTokenDTO
    {
        public long Id { get; set; }
        public long? AccountId { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string TokenString { get; set; } = string.Empty;
        public DateTime ExpirationTimeUtc { get; set; }

        public UserType? Type
        {
            get
            {
                if (AccountId > 0) return UserType.User;

                return null;
            }
        }

        public long? ActualId
        {
            get
            {
                if (AccountId > 0) return AccountId;

                return null;
            }
        }
    }
}
