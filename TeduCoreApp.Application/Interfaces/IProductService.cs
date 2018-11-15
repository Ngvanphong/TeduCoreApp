﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IProductService: IDisposable
    {
        Task<ProductViewModel> AddAsync(ProductViewModel product);

        void Update(ProductViewModel product);

        void UpdateDb(Product productDb);

        void Delete(int id);

        List<string> GetProductName(string productName);

        List<ProductViewModel> GetAll();

        List<ProductViewModel> GetAll(int? categoryId, string hotPromotion, string keyword, int page, int pageSize, out int totalRow);

        List<ProductViewModel> GetAllPaging(int page, int pageSize, out int totalRow);

        ProductViewModel GetById(int id);

        Product GetProductDbById(int id);

        List<ProductViewModel> GetHotProduct();

        List<ProductViewModel> GetAllHotProduct(int page, int pageSize, out int totalRow);

        List<ProductViewModel> GetPromotionProduct();

        List<ProductViewModel> GetAllPromotionProduct(int page, int pageSize, out int totalRow);

        List<ProductViewModel> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        List<ProductViewModel> GetAllByCategoryPaging(int CategoryId, int page, int pageSize, string sort, out int totalRow);

       List<ProductViewModel> GetAllByNamePaging(string Name, int page, int pageSize, string sort, out int totalRow);

       List<ProductViewModel> GetProductRelate(int CategoryId);

    
        void SaveChanges();
    }
}
