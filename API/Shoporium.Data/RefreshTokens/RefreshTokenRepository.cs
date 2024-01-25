using AutoMapper;
using Shoporium.Data._EntityFramework;
using Shoporium.Data._EntityFramework.Entities;
using Shoporium.Entities.DTO.Users;

namespace Shoporium.Data.RefreshTokens
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        protected readonly ShoporiumContext Context;
        protected readonly IMapper _mapper;

        public RefreshTokenRepository(ShoporiumContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        public RefreshTokenDTO? GetRefreshToken(string tokenString, string ipAddress)
        {
            var token = Context.RefreshTokens.FirstOrDefault(_ => _.TokenString == tokenString && _.IpAddress == ipAddress);
            if (token == null) return null;

            return _mapper.Map<RefreshTokenDTO>(token);
        }

        public void RemoveExpiredRefreshTokens(DateTime utcNow)
        {
            var tokens = Context.RefreshTokens.Where(_ => _.ExpirationTimeUtc < utcNow).ToList();
            if (!tokens.Any()) return;

            Context.RefreshTokens.RemoveRange(tokens);
            Context.SaveChanges();
        }

        public void RemoveRefreshToken(string tokenString)
        {
            var token = Context.RefreshTokens.FirstOrDefault(_ => _.TokenString == tokenString);
            if (token == null) return;

            Context.RefreshTokens.Remove(token);
            Context.SaveChanges();
        }

        public void SaveRefreshToken(RefreshTokenDTO token)
        {
            var existingToken = Context.RefreshTokens
                .FirstOrDefault(_ => _.UserId == token.UserId &&
                                     _.IpAddress == token.IpAddress);

            if (existingToken != null)
            {
                existingToken.TokenString = token.TokenString;
                existingToken.ExpirationTimeUtc = token.ExpirationTimeUtc;
            }
            else
            {
                var newToken = _mapper.Map<RefreshToken>(token);
                Context.RefreshTokens.Add(newToken);
            }

            Context.SaveChanges();
        }
    }
}
