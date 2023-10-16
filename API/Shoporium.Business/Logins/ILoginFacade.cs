using Shoporium.Entities.DTO.Account;
using Shoporium.Entities.Enums;

namespace Shoporium.Business.Logins
{
    public interface ILoginFacade
    {
        (string accessToken, string refreshToken) Authenticate(LoginModelDTO model, string ipAddress);
        (string accessToken, string refreshToken) GetTokens(UserType userType, long id, string ipAddress);
        void RemoveExpiredRefreshTokens(DateTime utcNow);
    }
}