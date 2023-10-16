using Microsoft.Extensions.Configuration;
using Shoporium.Data.Accounts;
using Shoporium.Data.Logins;
using Shoporium.Entities.DTO.Account;

namespace Shoporium.Business.Accounts
{
    public class AccountFacade : IAccountFacade
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;
        private readonly ILoginRepository _loginRepository;

        public AccountFacade(
            IConfiguration configuration,
            IAccountRepository accountRepository,
            ILoginRepository loginRepository)
        {
            _configuration = configuration;
            _accountRepository = accountRepository;
            _loginRepository = loginRepository;
        }

        public AccountDTO? GetAccountByEmail(string email)
        {
            return _accountRepository.GetAccountByEmail(email);
        }

        public bool IsValidAccountCredentials(long accountId, string password)
        {
            var loginDetail = _loginRepository.GetLoginDetail(accountId);

            if (loginDetail == null)
                throw new ArgumentException(); // todo

            CheckUserIsTemporarilyBlocked(loginDetail);

            var isValid = string.CompareOrdinal(loginDetail.PasswordHash, password) == 0;

            loginDetail.LastLoginAttemptUtc = DateTime.Now;
            loginDetail.FailedLoginAttempts = isValid ? 0 : loginDetail.FailedLoginAttempts + 1;
            _loginRepository.UpdateLoginDetail(loginDetail);

            return isValid;
        }

        private void CheckUserIsTemporarilyBlocked(LoginDetailDTO loginDetail)
        {
            int maxLoginAttempts = int.Parse(_configuration["MaxLoginAttempts"] ?? DefaultMaxLoginAttempts.ToString());
            int lockOutResetMinutes =
                int.Parse(_configuration["LockoutAutoResetMinutes"] ?? DefaultLockoutAutoResetMinutes.ToString());

            var isBlocked = loginDetail.FailedLoginAttempts >= maxLoginAttempts &&
                            (loginDetail.LastLoginAttemptUtc ?? DateTime.MinValue).AddMinutes(lockOutResetMinutes) >
                            DateTime.Now;

            if (isBlocked)
                throw new ArgumentException();
            //throw new UserIsTemporarilyBlockedException(String.Format(
            //    Resources.Account.Login.UserBlocked, lockOutResetMinutes));
        }

        const int DefaultMaxLoginAttempts = 4;
        const int DefaultLockoutAutoResetMinutes = 15;
    }
}
