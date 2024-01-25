using Shoporium.Entities.DTO.Users;

namespace Shoporium.Business.Users
{
    public interface IUserFacade
    {
        void Register(RegisterDTO model);
        UserDTO? GetUserByEmail(string email);
        bool IsValidUserCredentials(long userId, string password);
    }
}