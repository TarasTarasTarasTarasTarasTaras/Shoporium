using System.Text.Json.Serialization;

namespace Shoporium.Web.Models.User
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
