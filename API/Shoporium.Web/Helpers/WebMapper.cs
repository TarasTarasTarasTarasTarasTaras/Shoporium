using AutoMapper;
using Shoporium.Entities.DTO.Account;
using Shoporium.Web.Models.Account;

namespace Shoporium.Web.Helpers
{
    public class WebMapper : Profile
    {
        public WebMapper()
        {
            CreateMap<LoginModel, LoginDTO>();
            CreateMap<RegisterModel, RegisterDTO>();
            CreateMap<RegisterModel, LoginDTO>();
        }
    }
}
