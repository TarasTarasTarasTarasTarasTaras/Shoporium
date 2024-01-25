using Shoporium.Entities.DTO.Users;

namespace Shoporium.Data.Users
{
    public interface IUserRepository
    {
        void Register(RegisterDTO model);
        UserDTO? GetUserByEmail(string email);
        UserDTO? GetUser(long userId);
    }
}