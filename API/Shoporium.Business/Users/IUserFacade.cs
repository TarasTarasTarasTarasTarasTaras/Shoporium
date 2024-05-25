using Shoporium.Entities.DTO.Users;

namespace Shoporium.Business.Users
{
    public interface IUserFacade
    {
        void Register(RegisterDTO model);
        UserDTO? GetUserById(long userId);
        UserDTO? GetUserByEmail(string email);
        void UpdateUserInfo(UpdateUserInfoDTO model);
        int GetUserCity(long userId);
        void UpdateCityForUser(int cityId, long userId);
        bool IsValidUserCredentials(long userId, string password);
    }
}