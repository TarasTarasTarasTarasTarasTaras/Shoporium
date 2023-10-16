using AutoMapper;
using Shoporium.Data._EntityFramework.Models;
using Shoporium.Entities.DTO.Account;

namespace Shoporium.Business.Helpers
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<AccountDTO, Account>().ReverseMap();
            CreateMap<LoginDetailDTO, LoginDetail>().ReverseMap();
            CreateMap<TokenDTO, Token>().ReverseMap();
            CreateMap<RefreshTokenDTO, RefreshToken>().ReverseMap();
        }
    }
}
