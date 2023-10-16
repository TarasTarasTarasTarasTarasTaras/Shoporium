using Shoporium.Entities.DTO.Account;

namespace Shoporium.Business.Accounts
{
    public interface IAccountFacade
    {
        void Register(RegisterDTO model);
        AccountDTO? GetAccountByEmail(string email);
        bool IsValidAccountCredentials(long accountId, string password);
    }
}