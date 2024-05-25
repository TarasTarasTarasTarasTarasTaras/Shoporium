using Shoporium.Entities.DTO.Users;

namespace Shoporium.Data.Users
{
    public interface IUserRepository
    {
        void Register(RegisterDTO model);
        UserDTO? GetUserById(long userId);
        UserDTO? GetUserByEmail(string email);
        UserDTO? GetUser(long userId);
        void UpdateUserInfo(UpdateUserInfoDTO model);
        int GetUserCity(long userId);
        void UpdateCityForUser(int cityId, long userId);
    }
}