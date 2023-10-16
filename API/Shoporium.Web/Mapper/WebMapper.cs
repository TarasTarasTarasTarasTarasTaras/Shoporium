using AutoMapper;
using Shoporium.Entities.DTO.Account;
using Shoporium.Web.Models.Account;

namespace Shoporium.Web.Mapper
{
    public class WebMapper : Profile
    {
        public WebMapper()
        {
            CreateMap<LoginModel, LoginModelDTO>();
        }
    }
}
