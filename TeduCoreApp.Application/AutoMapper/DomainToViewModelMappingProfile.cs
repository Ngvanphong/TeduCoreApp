using AutoMapper;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
        }
    }
}