using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile:Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(c => new ProductCategory(c));
            CreateMap<FunctionViewModel, Function>()
               .ConstructUsing(c => new Function(c));
            CreateMap<ProductViewModel, Product>()
               .ConstructUsing(c => new Product(c));         
            CreateMap<ProductImageViewModel, ProductImage>()
              .ConstructUsing(c => new ProductImage(c));
            CreateMap<SizeViewModel, Size>()
              .ConstructUsing(c => new Size(c));
            CreateMap<ColorViewModel, Color>()
             .ConstructUsing(c => new Color(c));
            CreateMap<ProductQuantityViewModel, ProductQuantity>()
             .ConstructUsing(c => new ProductQuantity(c));

        }
    }
}
