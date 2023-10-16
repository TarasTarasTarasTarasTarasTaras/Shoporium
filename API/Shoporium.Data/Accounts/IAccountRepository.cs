using Shoporium.Entities.DTO.Account;

namespace Shoporium.Data.Accounts
{
    public interface IAccountRepository
    {
        void Register(RegisterDTO model);
        AccountDTO? GetAccountByEmail(string email);
        AccountDTO? GetAccount(long accountId);
    }
}