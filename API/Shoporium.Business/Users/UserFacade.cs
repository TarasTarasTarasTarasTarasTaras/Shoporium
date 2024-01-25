using Microsoft.Extensions.Configuration;
using Shoporium.Data.Logins;
using Shoporium.Data.Users;
using Shoporium.Entities.DTO.Users;

namespace Shoporium.Business.Users
{
    public class UserFacade : IUserFacade
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ILoginRepository _loginRepository;

        public UserFacade(
            IConfiguration configuration,
            IUserRepository userRepository,
            ILoginRepository loginRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _loginRepository = loginRepository;
        }

        public void Register(RegisterDTO model)
        {
            _userRepository.Register(model);
        }

        public UserDTO? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public bool IsValidUserCredentials(long userId, string password)
        {
            var loginDetail = _loginRepository.GetLoginDetail(userId);

            if (loginDetail == null)
                throw new ArgumentException(); // todo

            CheckUserIsTemporarilyBlocked(loginDetail);

            var isValid = string.CompareOrdinal(loginDetail.PasswordHash, password) == 0;

            loginDetail.LastLoginAttemptUtc = DateTime.Now;
            loginDetail.FailedLoginAttempts = isValid ? 0 : loginDetail.FailedLoginAttempts + 1;
            _loginRepository.UpdateLoginDetail(loginDetail.LoginDetailId, loginDetail.LastLoginAttemptUtc.Value, loginDetail.FailedLoginAttempts);

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
