using Shoporium.Data.Accounts;
using Shoporium.Data.RefreshTokens;
using Shoporium.Entities.DTO.Account;
using Shoporium.Entities.Options;
using System.Security.Claims;

namespace Shoporium.Business.Auth
{
    public class AccountAuthManager : AuthManager
    {
        private readonly IAccountRepository _accountRepository;

        public AccountAuthManager(
            JwtTokenOptions jwtTokenOptions,
            IRefreshTokenRepository refreshTokenRepository,
            IAccountRepository accountRepository)
            : base(jwtTokenOptions, refreshTokenRepository)
        {
            _accountRepository = accountRepository;
        }

        protected override Claim[] GenerateClaims(long id)
        {
            var account = _accountRepository.GetAccount(id);
            if (account == null)
                throw new KeyNotFoundException($"Account {id} doesn't exist.");

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new("CurrentAccountId", account.Id.ToString()),

                new(ClaimTypes.Email, account.Email),
                new("FirstName", account.FirstName),
                new("LastName", account.LastName),

                new(ClaimTypes.Role, account.UserType.ToString())
            };

            return claims.ToArray();
        }

        protected override RefreshTokenDTO PrepareRefreshToken(long id)
        {
            return new RefreshTokenDTO()
            {
                AccountId = id
            };
        }
    }
}
