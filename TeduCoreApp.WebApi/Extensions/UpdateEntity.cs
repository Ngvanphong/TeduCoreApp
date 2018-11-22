using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;

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
    }
}