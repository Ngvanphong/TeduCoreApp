using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class ProductCategoryService : IProductCategoryService
    {
        IRepository<ProductCategory, int> _productCategoryRepository;
        IUnitOfWork _unitOfWork;
        public ProductCategoryService(IRepository<ProductCategory, int> productCategoryRepository, IUnitOfWork unitOfWork)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
        }
        public ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm)
        {
            ProductCategory productCategory = Mapper.Map<ProductCategory>(productCategoryVm);
            _productCategoryRepository.Add(productCategory);
            return productCategoryVm;
        }

        public void Delete(int id)
        {
            _productCategoryRepository.Remove(id);
        }

        public List<ProductCategoryViewModel> GetAll()
        {
            List<ProductCategoryViewModel> lisProductCategoryVm = _productCategoryRepository.FindAll().OrderBy(x => x.ParentId)
                .ProjectTo<ProductCategoryViewModel>().ToList();
            return lisProductCategoryVm;
        }

        public List<ProductCategoryViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var ProductCategory = _productCategoryRepository.FindAll(x => x.Name.Contains(keyword)||x.Description.Contains(keyword)).OrderBy(x => x.ParentId);
                return ProductCategory.ProjectTo<ProductCategoryViewModel>().ToList();
            }
            else
            {
                return _productCategoryRepository.FindAll().OrderBy(x => x.ParentId).ProjectTo<ProductCategoryViewModel>().ToList();
            }
        }

        public List<ProductCategoryViewModel> GetAllByParentId(int parentId)
        {
            return _productCategoryRepository.FindAll(x => x.ParentId == parentId && x.Status == Status.Active).OrderBy(x => x.Name)
                .ProjectTo<ProductCategoryViewModel>().ToList();
        }

        public ProductCategoryViewModel GetById(int id)
        {
            return Mapper.Map<ProductCategoryViewModel>(_productCategoryRepository.FindById(id));
        }

        public List<ProductCategoryViewModel> GetHomeCategories(int top)
        {
           return _productCategoryRepository.FindAll(x=>x.Status==Status.Active&&x.HomeFlag==true,c=>c.Products)
                .OrderByDescending(x => x.HomeOrder).Take(top)
                .ProjectTo<ProductCategoryViewModel>().ToList();
            
        }

        public void ReOrder(int sourceId, int targetId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategoryViewModel productCategoryVm)
        {
            ProductCategory productCategogy = Mapper.Map<ProductCategory>(productCategoryVm);
            _productCategoryRepository.Update(productCategogy);
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            throw new NotImplementedException();
        }
    }
}
