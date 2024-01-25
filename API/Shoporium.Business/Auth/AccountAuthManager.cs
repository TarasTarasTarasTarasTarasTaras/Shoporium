using Shoporium.Data.RefreshTokens;
using Shoporium.Data.Users;
using Shoporium.Entities.DTO.Users;
using Shoporium.Entities.Options;
using System.Security.Claims;

namespace Shoporium.Business.Auth
{
    public class AccountAuthManager : AuthManager
    {
        private readonly IUserRepository _userRepository;

        public AccountAuthManager(
            JwtTokenOptions jwtTokenOptions,
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository)
            : base(jwtTokenOptions, refreshTokenRepository)
        {
            _userRepository = userRepository;
        }

        protected override Claim[] GenerateClaims(long id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null)
                throw new KeyNotFoundException($"Account {id} doesn't exist.");

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("CurrentUserId", user.Id.ToString()),

                new(ClaimTypes.Email, user.Email),
                new("FirstName", user.FirstName),
                new("LastName", user.LastName),

                new(ClaimTypes.Role, user.UserType.ToString())
            };

            return claims.ToArray();
        }

        protected override RefreshTokenDTO PrepareRefreshToken(long id)
        {
            return new RefreshTokenDTO()
            {
                UserId = id
            };
        }
    }
}
