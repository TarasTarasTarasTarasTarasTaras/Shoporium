using AutoMapper;
using Shoporium.Data._EntityFramework;
using Shoporium.Data._EntityFramework.Entities;
using Shoporium.Entities.DTO.Users;
using Shoporium.Entities.Enums;

namespace Shoporium.Data.Users
{
    public class UserRepository : IUserRepository
    {
        protected readonly IMapper _mapper;
        protected readonly ShoporiumContext Context;

        public UserRepository(ShoporiumContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        public void Register(RegisterDTO model)
        {
            var user = _mapper.Map<User>(model);
            user.Status = GeneralStatus.Active;
            user.UserType = UserType.User;
            Context.Users.Add(user);
            Context.SaveChanges();

            var loginDetail = new LoginDetail()
            {
                UserId = user.Id,
                Password = model.Password
            };
            Context.LoginDetails.Add(loginDetail);
            Context.SaveChanges();
        }

        public UserDTO? GetUserById(long userId)
        {
            var user = Context.Users
                .FirstOrDefault(_ => _.Status == GeneralStatus.Active && _.Id == userId);

            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public void UpdateUserInfo(UpdateUserInfoDTO model)
        {
            var entity = Context.Users.FirstOrDefault(u => u.Id == model.UserId);
            _mapper.Map(model, entity);

            Context.SaveChanges();
        }

        public UserDTO? GetUserByEmail(string email)
        {
            var user = Context.Users
                .FirstOrDefault(_ => _.Status == GeneralStatus.Active && _.Email == email);

            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public UserDTO? GetUser(long userId)
        {
            var user = Context
                .Users
                .FirstOrDefault(x => x.Id == userId);

            return user == null ? null : _mapper.Map<UserDTO>(user);
        }
    }
}
