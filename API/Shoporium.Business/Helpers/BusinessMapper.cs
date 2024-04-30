using AutoMapper;
using Shoporium.Data._EntityFramework.Entities;
using Shoporium.Entities.DTO.Products;
using Shoporium.Entities.DTO.Stores;
using Shoporium.Entities.DTO.Users;

namespace Shoporium.Business.Helpers
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<UserDTO, User>().ReverseMap();

            CreateMap<LoginDetailDTO, LoginDetail>()
                .ReverseMap()
            .ForPath(_ => _.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<TokenDTO, Token>().ReverseMap();

            CreateMap<RefreshTokenDTO, RefreshToken>().ReverseMap();

            CreateMap<RegisterDTO, User>();

            CreateMap<StoreDTO, Store>().ReverseMap();

            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.ProductPhotos, opt => opt.MapFrom(src => src.ProductPhotos.Select(photo => new ProductPhoto { Name = photo })))
                .ReverseMap()
                .ForMember(dest => dest.ProductPhotos, opt => opt.MapFrom(src => src.ProductPhotos.Select(photo => photo.Name)));

            CreateMap<ProductPhoto, string>().ConstructUsing(src => src.Name);

        }
    }
}
