using Shoporium.Entities.DTO.Account;

namespace Shoporium.Business.Accounts
{
    public interface IAccountFacade
    {
        AccountDTO? GetAccountByEmail(string email);
        bool IsValidAccountCredentials(long accountId, string password);
    }
}