using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Application.Interfaces
{
   public interface IProductQuantityService:IDisposable
    {
        void Add(ProductQuantityViewModel productQuantityVm);

        void Update(ProductQuantityViewModel productQuantityVm);

        void UpdateDb(ProductQuantity productQuantity);

        void Delete(int productId, int sizeId, int colorId);

        void DeleteByProductId(int productId);

        List<ProductQuantityViewModel> GetAll(int productId);

        List<ProductQuantityViewModel> GetAll(int productId, int? sizeId, int? colorId);

        ProductQuantityViewModel GetSingle(int productId, int sizeId, int colorId);

        ProductQuantity GetSingleDb(int productId, int sizeId, int colorId);

        bool CheckExist(int productId, int sizeId, int colorId);
        //Size
        List<SizeViewModel> GetListSize();

        void AddSize(SizeViewModel sizeVm);

        void DeleteSize(int id);

        SizeViewModel GetSizeById(int id);

        void UpdateSize(SizeViewModel size);

        List<SizeViewModel> GetSizeByProductId(int productId);

        List<SizeViewModel> GetSizeByColor(int productId,int colorId);
        //Color
        List<ColorViewModel> GetListColor();

        void AddColor(ColorViewModel colorVm);

        void DeleteColor(int id);

        ColorViewModel GetColorById(int id);

        void UpdateColor(ColorViewModel color);

        List<ColorViewModel> GetColorByProductId(int productId);

        void SaveChanges();


    }
}
