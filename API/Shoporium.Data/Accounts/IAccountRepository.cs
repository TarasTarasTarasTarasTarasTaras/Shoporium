using Shoporium.Entities.DTO.Account;

namespace Shoporium.Data.Accounts
{
    public interface IAccountRepository
    {
        AccountDTO? GetAccountByEmail(string email);
        AccountDTO? GetAccount(long accountId);
    }
}