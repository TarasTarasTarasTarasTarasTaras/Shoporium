using AutoMapper;
using Microsoft.Identity.Client;
using Shoporium.Data._EntityFramework;
using Shoporium.Data._EntityFramework.Models;
using Shoporium.Entities.DTO.Account;
using Shoporium.Entities.Enums;

namespace Shoporium.Data.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        protected readonly ShoporiumContext Context;
        protected readonly IMapper _mapper;

        public AccountRepository(ShoporiumContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        public void Register(RegisterDTO model)
        {
            var account = _mapper.Map<Account>(model);
            account.Status = Status.Active;
            account.UserType = UserType.User;
            Context.Accounts.Add(account);
            Context.SaveChanges();

            var loginDetail = new LoginDetail()
            {
                AccountId = account.Id,
                Password = model.Password
            };
            Context.LoginDetails.Add(loginDetail);
            Context.SaveChanges();
        }

        public AccountDTO? GetAccountByEmail(string email)
        {
            var account = Context.Accounts
                .FirstOrDefault(_ => _.Status == Status.Active && _.Email == email);

            return account == null ? null : _mapper.Map<AccountDTO>(account);
        }

        public AccountDTO? GetAccount(long accountId)
        {
            var account = Context
                .Accounts
                .FirstOrDefault(x => x.Id == accountId);

            return account == null ? null : _mapper.Map<AccountDTO>(account);
        }
    }
}
