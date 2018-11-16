using AutoMapper;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.Data.ViewModels.Product;

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
        }
    }
}