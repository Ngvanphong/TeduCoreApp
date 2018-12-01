using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Application.Interfaces
{
   public interface IProductImageService:IDisposable
    {
        void Add(ProductImageViewModel productImageVm);

        void Update(ProductImageViewModel productImageVm);

        void Delete(int id);

        ProductImageViewModel GetById(int id);

        List<ProductImageViewModel> GetProductImageByProdutId(int productId);

        List<ProductImageViewModel> GetProductImageContentByProdutId(int productId);

        void SaveChanges();
    }
}
