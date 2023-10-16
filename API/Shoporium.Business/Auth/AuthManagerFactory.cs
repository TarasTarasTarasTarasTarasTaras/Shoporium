using Shoporium.Data.Accounts;
using Shoporium.Data.RefreshTokens;
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
        private readonly IAccountRepository _accountRepository;

        public AuthManagerFactory(
            JwtTokenOptions jwtTokenOptions,
            IRefreshTokenRepository refreshTokenRepository,
            IAccountRepository accountRepository)
        {
            _jwtTokenOptions = jwtTokenOptions;
            _refreshTokenRepository = refreshTokenRepository;
            _accountRepository = accountRepository;
        }

        public IAuthManager GetAuthManager(UserType? role)
        {
            switch (role)
            {
                case UserType.Account:
                    return new AccountAuthManager(_jwtTokenOptions, _refreshTokenRepository, _accountRepository);
                default:
                    throw new ArgumentException("Invalid authentication type");
            }
        }
    }
}
