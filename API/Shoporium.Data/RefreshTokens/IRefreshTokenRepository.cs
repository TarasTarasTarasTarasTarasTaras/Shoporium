using Shoporium.Entities.DTO.Account;

namespace Shoporium.Data.RefreshTokens
{
    public interface IRefreshTokenRepository
    {
        RefreshTokenDTO? GetRefreshToken(string tokenString, string ipAddress);
        void RemoveExpiredRefreshTokens(DateTime utcNow);
        void RemoveRefreshToken(string tokenString);
        void SaveRefreshToken(RefreshTokenDTO token);
    }
}