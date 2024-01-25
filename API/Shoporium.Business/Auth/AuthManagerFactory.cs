using Shoporium.Data.RefreshTokens;
using Shoporium.Data.Users;
using Shoporium.Entities.Enums;
using Shoporium.Entities.Options;

namespace Shoporium.Business.Auth
{
    public interface IAuthManagerFactory
    {
        IAuthManager GetAuthManager(UserType? role);
    }

    public class AuthManagerFactory : IAuthManagerFactory
    {
        protected JwtTokenOptions _jwtTokenOptions;
        protected IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;

        public AuthManagerFactory(
            JwtTokenOptions jwtTokenOptions,
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository)
        {
            _jwtTokenOptions = jwtTokenOptions;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
        }

        public IAuthManager GetAuthManager(UserType? role)
        {
            switch (role)
            {
                case UserType.User:
                    return new AccountAuthManager(_jwtTokenOptions, _refreshTokenRepository, _userRepository);
                default:
                    throw new ArgumentException("Invalid authentication type");
            }
        }
    }
}
