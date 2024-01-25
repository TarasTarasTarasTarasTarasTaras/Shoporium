using Shoporium.Business.Auth;
using Shoporium.Entities.Enums;
using Shoporium.Data.RefreshTokens;
using Shoporium.Entities.DTO.Users;
using Shoporium.Business.Users;

namespace Shoporium.Business.Logins
{
    public class LoginFacade : ILoginFacade
    {
        private readonly IUserFacade _userFacade;
        private readonly IAuthManagerFactory _authManagerFactory;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginFacade(
            IUserFacade userFacade,
            IAuthManagerFactory authManagerFactory,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userFacade = userFacade;
            _authManagerFactory = authManagerFactory;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public void Register(RegisterDTO model)
        {
            var user = _userFacade.GetUserByEmail(model.Email);
            if (user != null && user.IsEmailVerified)
                throw new ArgumentException();

            _userFacade.Register(model);
        }

        public (string accessToken, string refreshToken) Authenticate(LoginDTO model, string ipAddress, bool afterRegistration = false)
        {
            // todo: check count of accounts with not verified email
            var user = _userFacade.GetUserByEmail(model.Email);

            if (user == null || !_userFacade.IsValidUserCredentials(user.Id, model.Password))
                throw new ArgumentException();
                //throw new UnauthorizedException(Resources.Account.Login.InvalidUser);

            //if (!account!.IsEmailVerified && !afterRegistration)
                //throw new EmailIsNotConfirmedException();

            var tokens = GetTokens(UserType.User, user.Id, ipAddress);

            //_logger.LogInformationWithUserId("User logged in the system.", user.UserId);
            return tokens;
        }

        public (string accessToken, string refreshToken) GetTokens(UserType userType, long id, string ipAddress)
        {
            var authManager = _authManagerFactory.GetAuthManager(userType);
            return authManager.Authenticate(id, FormatIpAddress(ipAddress), DateTime.UtcNow);
        }

        public RefreshTokenDTO? GetRefreshToken(string token, string ipAddress)
        {
            return _refreshTokenRepository.GetRefreshToken(token, FormatIpAddress(ipAddress));
        }

        public (string accessToken, string refreshToken) RefreshTokens(RefreshTokenDTO refreshToken)
        {
            var authManager = _authManagerFactory.GetAuthManager(refreshToken.Type);
            return authManager.Authenticate((refreshToken.ActualId ?? 0), FormatIpAddress(refreshToken.IpAddress), DateTime.UtcNow);
        }

        public void RemoveExpiredRefreshTokens(DateTime utcNow)
        {
            _refreshTokenRepository.RemoveExpiredRefreshTokens(utcNow);
        }

        private static string FormatIpAddress(string ipAddress)
        {
            return ipAddress?.Split(':')[0] ?? "";
        }
    }
}
