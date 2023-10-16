using AutoMapper;
using Shoporium.Data._EntityFramework;
using Shoporium.Data._EntityFramework.Models;
using Shoporium.Entities.DTO.Account;

namespace Shoporium.Data.Logins
{
    public class LoginRepository : ILoginRepository
    {
        protected readonly ShoporiumContext Context;
        protected readonly IMapper _mapper;

        public LoginRepository(ShoporiumContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        public LoginDetailDTO? GetLoginDetail(long accountId)
        {
            var loginDetail = Context.LoginDetails
                .FirstOrDefault(x => x.AccountId == accountId);

            return loginDetail == null ? null : _mapper.Map<LoginDetailDTO>(loginDetail);
        }

        public void UpdateLoginDetail(long loginDetailId, DateTime lastLoginAttemptUtc, int failedLoginAttempts)
        {
            var entity = Context.LoginDetails.FirstOrDefault(l => l.LoginDetailId == loginDetailId);

            if (entity == null)
                throw new KeyNotFoundException("Update login detail failed. Login detail is not found.");

            entity.LastLoginAttemptUtc = lastLoginAttemptUtc;
            entity.FailedLoginAttempts = failedLoginAttempts;
            Context.SaveChanges();
        }
    }
}
