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

        public void UpdateLoginDetail(LoginDetailDTO login)
        {
            var entity = _mapper.Map<LoginDetail>(login);
            Context.LoginDetails.Update(entity);
            Context.SaveChanges();
        }
    }
}
