using AutoMapper;
using Shoporium.Data._EntityFramework.Entities;
using Shoporium.Entities.DTO.Account;

namespace Shoporium.Business.Helpers
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<AccountDTO, Account>().ReverseMap();

            CreateMap<LoginDetailDTO, LoginDetail>()
                .ReverseMap()
            .ForPath(_ => _.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<TokenDTO, Token>().ReverseMap();

            CreateMap<RefreshTokenDTO, RefreshToken>().ReverseMap();

            CreateMap<RegisterDTO, Account>();
        }
    }
}
