using System.Text.Json.Serialization;

namespace Shoporium.Web.Models.Account
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
