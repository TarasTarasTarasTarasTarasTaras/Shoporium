using Shoporium.Entities.DTO.Account;

namespace Shoporium.Data.Logins
{
    public interface ILoginRepository
    {
        LoginDetailDTO? GetLoginDetail(long accountId);
        void UpdateLoginDetail(LoginDetailDTO login);
    }
}