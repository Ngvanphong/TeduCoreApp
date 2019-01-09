﻿using AutoMapper;

using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Data.ViewModels.BillUserAnnoucement;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.Data.ViewModels.Contact;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.Data.ViewModels.Identity;
using TeduCoreApp.Data.ViewModels.Page;
using TeduCoreApp.Data.ViewModels.Pantner;
using TeduCoreApp.Data.ViewModels.Permission;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Data.ViewModels.Slide;
using TeduCoreApp.Data.ViewModels.Subcrible;
using TeduCoreApp.Data.ViewModels.SystemConfig;
using TeduCoreApp.Data.ViewModels.Tag;

namespace TeduCoreApp.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
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
            CreateMap<AppRoleViewModel, AppRole>()
            .ConstructUsing(c => new AppRole(c.Name, c.Description));
            CreateMap<AppUserViewModel, AppUser>()
             .ConstructUsing(c => new AppUser(c));
            CreateMap<PermissionViewModel, Permission>()
             .ConstructUsing(c => new Permission(c));
            CreateMap<BlogViewModel, Blog>()
            .ConstructUsing(c => new Blog(c));
            CreateMap<TagViewModel, Tag>()
           .ConstructUsing(c => new Tag(c));
            CreateMap<BlogTagViewModel, BlogTag>()
           .ConstructUsing(c => new BlogTag(c));
            CreateMap<BlogImageViewModel, BlogImage>()
            .ConstructUsing(c => new BlogImage(c));
            CreateMap<SlideViewModel, Slide>()
             .ConstructUsing(c => new Slide(c));
            CreateMap<BillViewModel, Bill>()
            .ConstructUsing(c => new Bill(c));
            CreateMap<BillDetailViewModel, BillDetail>()
             .ConstructUsing(c => new BillDetail(c));
            CreateMap<BillUserAnnoucementViewModel, BillUserAnnoucement>()
            .ConstructUsing(c => new BillUserAnnoucement(c));
            CreateMap<WholePriceViewModel, WholePrice>()
            .ConstructUsing(c => new WholePrice(c));
            CreateMap<AdvertistmentViewModel, Advertistment>()
            .ConstructUsing(c => new Advertistment(c));
            CreateMap<AdvertistmentPageViewModel, AdvertistmentPage>()
            .ConstructUsing(c => new AdvertistmentPage(c));
            CreateMap<AdvertistmentPositionViewModel, AdvertistmentPosition>()
           .ConstructUsing(c => new AdvertistmentPosition(c));
            CreateMap<ContactViewModel, Contact>()
           .ConstructUsing(c => new Contact(c));
            CreateMap<PageViewModel, Page>()
          .ConstructUsing(c => new Page(c));
            CreateMap<PageImageViewModel, PageImage>()
          .ConstructUsing(c => new PageImage(c));
            CreateMap<PantnerViewModel, Pantner>()
          .ConstructUsing(c => new Pantner(c));
            CreateMap<SubcribleViewModel, Subcrible>()
          .ConstructUsing(c => new Subcrible(c));
            CreateMap<SystemConfigViewModel, SystemConfig>()
         .ConstructUsing(c => new SystemConfig(c));
        }
    }
}