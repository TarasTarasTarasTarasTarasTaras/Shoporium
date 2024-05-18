using AutoMapper;
using Shoporium.Entities.DTO.Products;
using Shoporium.Entities.DTO.Stores;
using Shoporium.Entities.DTO.Users;
using Shoporium.Web.Models.Products;
using Shoporium.Web.Models.Stores;
using Shoporium.Web.Models.User;

namespace Shoporium.Web.Helpers
{
    public class WebMapper : Profile
    {
        public WebMapper()
        {
            CreateMap<UpdateUserInfoModel, UpdateUserInfoDTO>();

            CreateMap<LoginModel, LoginDTO>();
            CreateMap<RegisterModel, RegisterDTO>();
            CreateMap<RegisterModel, LoginDTO>();

            CreateMap<StoreDTO, CreateStoreModel>()
                .ReverseMap()
                .ForPath(_ => _.MainPhoto, opt => opt.MapFrom(src => src.MainPhoto == null ? "" : src.MainPhoto.FileName))
                .ForPath(_ => _.BackgroundPhoto, opt => opt.MapFrom(src => src.BackgroundPhoto == null ? "" : src.BackgroundPhoto.FileName));

            CreateMap<ProductDTO, CreateProductModel>()
                .ReverseMap()
                .ForMember(dest => dest.ProductPhotos, opt => opt.MapFrom(src => src.ProductPhotos.Select(photo => photo == null ? "" : photo.FileName)));
        }
    }
}
