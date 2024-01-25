using Shoporium.Entities.DTO.Users;

namespace Shoporium.Data.Logins
{
    public interface ILoginRepository
    {
        LoginDetailDTO? GetLoginDetail(long userId);
        void UpdateLoginDetail(long loginDetailId, DateTime lastLoginAttemptUtc, int failedLoginAttempts);
    }
}