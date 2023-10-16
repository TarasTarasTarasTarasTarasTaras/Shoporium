using Shoporium.Business.Auth;
using Shoporium.Business.Accounts;
using Shoporium.Entities.DTO.Account;
using Shoporium.Entities.Enums;
using Shoporium.Data.RefreshTokens;

namespace Shoporium.Business.Logins
{
    public class LoginFacade : ILoginFacade
    {
        private readonly IAccountFacade _accountFacade;
        private readonly IAuthManagerFactory _authManagerFactory;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginFacade(
            IAccountFacade accountFacade,
            IAuthManagerFactory authManagerFactory,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _accountFacade = accountFacade;
            _authManagerFactory = authManagerFactory;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public (string accessToken, string refreshToken) Authenticate(LoginModelDTO model, string ipAddress)
        {
            var account = _accountFacade.GetAccountByEmail(model.Email);

            if (account == null || !_accountFacade.IsValidAccountCredentials(account.Id, model.Password))
                throw new ArgumentException();
                //throw new UnauthorizedException(Resources.Account.Login.InvalidUser);

            if (!account!.IsEmailVerified)
                throw new ArgumentException();
                //throw new EmailIsNotConfirmedException();

            var tokens = GetTokens(UserType.Account, account.Id, ipAddress);

            //_logger.LogInformationWithUserId("User logged in the system.", user.UserId);
            return tokens;
        }

        public (string accessToken, string refreshToken) GetTokens(UserType userType, long id, string ipAddress)
        {
            var authManager = _authManagerFactory.GetAuthManager(userType);
            return authManager.Authenticate(id, FormatIpAddress(ipAddress), DateTime.UtcNow);
        }

        public void RemoveExpiredRefreshTokens(DateTime utcNow)
        {
            _refreshTokenRepository.RemoveExpiredRefreshTokens(utcNow);
        }

        private static string? FormatIpAddress(string ipAddress)
        {
            return ipAddress?.Split(':')[0];
        }
    }
}
