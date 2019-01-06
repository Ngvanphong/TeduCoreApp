using System;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.Data.ViewModels.Identity;
using TeduCoreApp.Data.ViewModels.Pantner;
using TeduCoreApp.Data.ViewModels.Permission;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Data.ViewModels.Slide;

namespace TeduCoreApp.WebApi.Extensions
{
    public static class UpdateEntity
    {
        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVm)
        {          
            productCategory.Name = productCategoryVm.Name;
            productCategory.SeoAlias = productCategoryVm.SeoAlias;
            productCategory.ParentId = productCategoryVm.ParentId;
            productCategory.SortOrder = productCategoryVm.SortOrder;
            productCategory.Description = productCategoryVm.Description;
            productCategory.Image = productCategoryVm.Image;
            productCategory.HomeFlag = productCategoryVm.HomeFlag;
            productCategory.SeoKeywords = productCategoryVm.SeoKeywords;
            productCategory.SeoDescription = productCategoryVm.SeoDescription;
            productCategory.Status = productCategoryVm.Status;
            productCategory.HomeOrder = productCategoryVm.HomeOrder;
            productCategory.SeoPageTitle = productCategoryVm.SeoPageTitle;
        }

        public static void UpdateUser(this AppUser appUser, AppUserViewModel appUserVm)
        {
            appUser.Address = appUserVm.Address;
            appUser.Avatar = appUserVm.Avatar;
            appUser.BirthDay = appUserVm.BirthDay;
            appUser.Email = appUserVm.Email;
            appUser.Gender = appUserVm.Gender;
            appUser.FullName = appUserVm.FullName;
            appUser.Status = appUserVm.Status;
            appUser.PhoneNumber = appUserVm.PhoneNumber;
            appUser.UserName = appUserVm.UserName;
        }

        public static void UpdatePermission(this Permission permission,PermissionViewModel permissionVm)
        {
            permission.RoleId = permissionVm.RoleId;
            permission.CanCreate = permissionVm.CanCreate;
            permission.CanRead = permissionVm.CanRead;
            permission.CanUpdate = permissionVm.CanUpdate;
            permission.CanDelete = permissionVm.CanDelete;
            permission.FunctionId = permissionVm.FunctionId;        
        }
        public static void UpdateAppRole(this AppRole appRole, AppRoleViewModel appRoleVm)
        {
            appRole.Name = appRoleVm.Name;
            appRole.Description = appRoleVm.Description;
        }
        public static void UpdateBlog(this Blog blog,BlogViewModel blogVm)
        {
            blog.Name = blogVm.Name;
            blog.Image = blogVm.Image;
            blog.Status = blogVm.Status;
            blog.HomeFlag = blogVm.HomeFlag;
            blog.HotFlag = blogVm.HotFlag;
            blog.SeoPageTitle = blogVm.SeoPageTitle;
            blog.SeoAlias = blogVm.SeoAlias;
            blog.SeoKeywords = blogVm.SeoKeywords;
            blog.SeoDescription = blogVm.SeoDescription;
            blog.Tags = blogVm.Tags;
            blog.Content = blogVm.Content;
            blog.Description = blogVm.Description;         
        }

        public static void UpdateSlide(this Slide slide, SlideViewModel slideVm)
        {
            slide.Name = slideVm.Name;
            slide.Image = slideVm.Image;
            slide.Status = slideVm.Status;
            slide.Url = slideVm.Url;
            slide.Content = slideVm.Content;
            slide.Description = slideVm.Description;
            slide.DisplayOrder = slideVm.DisplayOrder;
            slide.OrtherPageHome = slideVm.OrtherPageHome;         
        }

        public static void UpdateBillDetail(this BillDetail billDetail, BillDetailViewModel billDetailVm)
        {
         billDetail.BillId = billDetailVm.BillId;
         billDetail.ProductId = billDetailVm.ProductId;
         billDetail.Quantity = billDetailVm.Quantity;
         billDetail.Price = billDetailVm.Price;
         billDetail.ColorId = billDetailVm.ColorId;
         billDetail.SizeId = billDetailVm.SizeId;
        }
        
        public static void UpdateAdvertistment(this Advertistment advertistment, AdvertistmentViewModel advertistmentVm)
        {
         advertistment.Name = advertistmentVm.Name;
         advertistment.Description = advertistmentVm.Description;
         advertistment.Image = advertistmentVm.Image;
         advertistment.Url = advertistmentVm.Url;
         advertistment.PositionId = advertistmentVm.PositionId;
         advertistment.PageId = advertistmentVm.PageId;
         advertistment.Status = advertistmentVm.Status;
         advertistment.DateModified = DateTime.Now;
         advertistment.SortOrder = advertistmentVm.SortOrder;
        }

        public static void UpdatePantner(this Pantner pantner, PantnerViewModel pantnerVm)
        {
            pantner.Name = pantnerVm.Name;
            pantner.Image = pantnerVm.Image;
            pantner.Status = pantnerVm.Status;
            pantner.Url = pantnerVm.Url;            
        }


    }
}