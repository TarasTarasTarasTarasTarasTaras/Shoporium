using Shoporium.Entities.DTO.Users;
using Shoporium.Entities.Enums;

namespace Shoporium.Business.Logins
{
    public interface ILoginFacade
    {
        void Register(RegisterDTO model);
        (string accessToken, string refreshToken) Authenticate(LoginDTO model, string ipAddress, bool afterRegistration = false);
        (string accessToken, string refreshToken) GetTokens(UserType userType, long id, string ipAddress);
        void RemoveExpiredRefreshTokens(DateTime utcNow);
        RefreshTokenDTO? GetRefreshToken(string token, string ipAddress);
        (string accessToken, string refreshToken) RefreshTokens(RefreshTokenDTO refreshToken);
    }
}