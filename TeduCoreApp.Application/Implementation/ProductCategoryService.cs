using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class ProductCategoryService : IProductCategoryService
    {
        private IRepository<ProductCategory, int> _productCategoryRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductCategoryService(IRepository<ProductCategory, int> productCategoryRepository, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm)
        {
            ProductCategory productCategory = _mapper.Map<ProductCategory>(productCategoryVm);
            _productCategoryRepository.Add(productCategory);
            return productCategoryVm;
        }

        public void Delete(int id)
        {
            _productCategoryRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ProductCategoryViewModel> GetAll()
        {
            List<ProductCategoryViewModel> lisProductCategoryVm =_mapper.Map<List<ProductCategoryViewModel>>(_productCategoryRepository.FindAll()
                .OrderBy(x => x.ParentId) .ToList());
            return lisProductCategoryVm;
        }

        public List<ProductCategoryViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var productCategory = _productCategoryRepository.FindAll(x => x.Name.Contains(keyword) || x.Description.Contains(keyword)).OrderBy(x=>x.ParentId)
                    .ToList();
                return _mapper.Map<List<ProductCategoryViewModel>>(productCategory);
            }
            else
            {
                return _mapper.Map<List<ProductCategoryViewModel>>(_productCategoryRepository.FindAll().OrderBy(x => x.ParentId).ToList());
            }
        }

        public List<ProductCategoryViewModel> GetAllByParentId(int parentId)
        {
            return _mapper.Map<List<ProductCategoryViewModel>>(_productCategoryRepository.FindAll(x => x.ParentId == parentId && x.Status == Status.Active)
                .OrderBy(x => x.Name).ToList());

        }

        public ProductCategoryViewModel GetById(int id)
        {
            return _mapper.Map<ProductCategoryViewModel>(_productCategoryRepository.FindById(id));
        }

        public ProductCategory GetByIdDb(int id)
        {
            return _productCategoryRepository.FindById(id);
        }

        public List<ProductCategoryViewModel> GetCategoryFooter(int top)
        {
            var all = _productCategoryRepository.FindAll(x=>x.Status==Status.Active);
            var parentAll = _productCategoryRepository.FindAll(x => x.ParentId == null&& x.Status == Status.Active);
            List<ProductCategory> parent = new List<ProductCategory>();
            foreach(var item in parentAll)
            {
                if (all.Except(parentAll).Any(x => x.ParentId == item.Id)) parent.Add(item);
            }
            var listCategoryHasHref = all.Except(parent).OrderBy(x=>x.Name).Take(top);
            return _mapper.Map<List<ProductCategoryViewModel>>(listCategoryHasHref.ToList());
        }

        public List<ProductCategoryViewModel> GetHomeCategories(int top)
        {
            return _mapper.Map<List<ProductCategoryViewModel>>(_productCategoryRepository.FindAll(x => x.Status == Status.Active && x.HomeFlag == true, c => c.Products)
                 .OrderByDescending(x => x.HomeOrder).Take(top).ToList());                
        }

        public void ReOrder(int sourceId, int targetId)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategoryViewModel productCategoryVm)
        {
            ProductCategory productCategogy = _mapper.Map<ProductCategory>(productCategoryVm);
            _productCategoryRepository.Update(productCategogy);
        }

        public void UpdateDb(ProductCategory productCategory)
        {
            _productCategoryRepository.Update(productCategory);
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            throw new NotImplementedException();
        }
    }
}