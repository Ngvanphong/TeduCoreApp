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
        }
    }
}
