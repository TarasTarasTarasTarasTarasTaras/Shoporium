using System.Text.Json.Serialization;

namespace Shoporium.Entities.Options
{
    public class JwtTokenOptions
    {
        public const string JwtToken = "JwtToken";

        [JsonPropertyName("secret")]
        public string Secret { get; set; } = string.Empty;

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; } = string.Empty;

        [JsonPropertyName("audience")]
        public string Audience { get; set; } = string.Empty;

        [JsonPropertyName("accessTokenExpiration")]
        public int AccessTokenExpiration { get; set; }

        [JsonPropertyName("refreshTokenExpiration")]
        public int RefreshTokenExpiration { get; set; }
    }
}
