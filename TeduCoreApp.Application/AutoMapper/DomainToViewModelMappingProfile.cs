using AutoMapper;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.Data.ViewModels.Identity;
using TeduCoreApp.Data.ViewModels.Permission;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Data.ViewModels.Tag;

namespace TeduCoreApp.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Function, FunctionViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductImage, ProductImageViewModel>();
            CreateMap<Size, SizeViewModel>();
            CreateMap<Color, ColorViewModel>();
            CreateMap<ProductQuantity, ProductQuantityViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<Permission, PermissionViewModel>();
            CreateMap<Tag, TagViewModel>();
            CreateMap<BlogTag, BlogTagViewModel>();
            CreateMap<Blog, BlogViewModel>();
        }
    }
}