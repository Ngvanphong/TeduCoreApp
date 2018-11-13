using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class ProductImageService : IProductImageService
    {
        private IRepository<ProductImage, int> _productImageRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ProductImageService(IRepository<ProductImage, int> productImageRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Add(ProductImageViewModel productImageVm)
        {
            _productImageRepository.Add(_mapper.Map<ProductImage>(productImageVm));
        }

        public void Delete(int id)
        {
            _productImageRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ProductImageViewModel GetById(int id)
        {
            return _mapper.Map<ProductImageViewModel>(_productImageRepository.FindById(id));
        }

        public List<ProductImageViewModel> GetProductImageByProdutId(int productId)
        {
            return _mapper.Map<List<ProductImageViewModel>>(_productImageRepository.FindAll(x => x.ProductId == productId)
                .OrderByDescending(i => i.Id).ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductImageViewModel productImageVm)
        {
            _productImageRepository.Update(_mapper.Map<ProductImage>(productImageVm));
        }
    }
}
