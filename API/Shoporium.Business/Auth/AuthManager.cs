using Microsoft.IdentityModel.Tokens;
using Shoporium.Data.RefreshTokens;
using Shoporium.Entities.DTO.Account;
using Shoporium.Entities.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Shoporium.Business.Auth
{
    public interface IAuthManager
    {
        (string accessToken, string refreshToken) Authenticate(long id, string ipAddress, DateTime now);

    }

    public abstract class AuthManager : IAuthManager
    {
        private readonly JwtTokenOptions _jwtTokenOptions;
        private readonly byte[] _secret;

        private readonly IRefreshTokenRepository _refreshTokenRepository;

        protected AuthManager(
            JwtTokenOptions jwtTokenOptions,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtTokenOptions = jwtTokenOptions;
            _secret = Encoding.ASCII.GetBytes(_jwtTokenOptions.Secret);

            _refreshTokenRepository = refreshTokenRepository;
        }

        protected abstract Claim[] GenerateClaims(long id);
        protected abstract RefreshTokenDTO PrepareRefreshToken(long id);

        public (string accessToken, string refreshToken) Authenticate(long id, string ipAddress, DateTime now)
        {
            var accessToken = GenerateAccessToken(id, now);
            var refreshToken = AddRefreshToken(id, ipAddress, now);

            return (accessToken, refreshToken);
        }

        private string GenerateAccessToken(long id, DateTime now)
        {
            now = now.ToUniversalTime();

            var claims = GenerateClaims(id);
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            var jwtToken = new JwtSecurityToken(
                _jwtTokenOptions.Issuer,
                shouldAddAudienceClaim ? _jwtTokenOptions.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(_jwtTokenOptions.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private string AddRefreshToken(long id, string ipAddress, DateTime now)
        {
            var refreshToken = PrepareRefreshToken(id);
            refreshToken.IpAddress = ipAddress;
            refreshToken.TokenString = GenerateRefreshTokenString();
            refreshToken.ExpirationTimeUtc = now.ToUniversalTime().AddMinutes(_jwtTokenOptions.RefreshTokenExpiration);

            _refreshTokenRepository.SaveRefreshToken(refreshToken);
            return refreshToken.TokenString;
        }

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
