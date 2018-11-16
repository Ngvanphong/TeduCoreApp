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
  public class ProductQuantityService:IProductQuantityService
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private IRepository<ProductQuantity, int> _produtQuantityRepository;
        private IRepository<Size, int> _sizeRepository;
        private IRepository<Color, int> _colorRepository;

        public ProductQuantityService(IMapper mapper, IUnitOfWork unitOfWork,
            IRepository<ProductQuantity, int> produtQuantityRepository, IRepository<Size, int> sizeRepository, IRepository<Color, int> colorRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _produtQuantityRepository = produtQuantityRepository;
            _sizeRepository = sizeRepository;
            _colorRepository = colorRepository;
        }

        public void Add(ProductQuantityViewModel productQuantityVm)
        {
            _produtQuantityRepository.Add(_mapper.Map<ProductQuantity>(productQuantityVm));
        }

        public void AddColor(ColorViewModel colorVm)
        {
            _colorRepository.Add(_mapper.Map<Color>(colorVm));
        }

        public void AddSize(SizeViewModel sizeVm)
        {
            _sizeRepository.Add(_mapper.Map<Size>(sizeVm));
        }

        public bool CheckExist(int productId, int sizeId, int colorId)
        {
            List<ProductQuantity> listProductQuantity = _produtQuantityRepository
                 .FindAll(x => x.ProductId == productId && x.SizeId == sizeId && x.ColorId == colorId).ToList();
            if (listProductQuantity.Count() > 0) return true;
            return false;
        }

        public void Delete(int productId, int sizeId, int colorId)
        {
            ProductQuantity productQuantity = _produtQuantityRepository
                .FindSingle(x => x.ProductId == productId && x.SizeId == sizeId && x.ColorId == colorId);
            _produtQuantityRepository.Remove(productQuantity);

        }

        public void DeleteByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public void DeleteColor(int id)
        {
            _colorRepository.Remove(id);
        }

        public void DeleteSize(int id)
        {
            _sizeRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ProductQuantityViewModel> GetAll(int productId, int? sizeId, int? colorId)
        {
            List<ProductQuantity> listProductQuantity = _produtQuantityRepository
                .FindAll(x => x.ProductId == productId).ToList();
            if (sizeId.HasValue)
            {
                listProductQuantity = listProductQuantity.Where(x => x.SizeId == sizeId).ToList();
            }
            if (colorId.HasValue)
            {
                listProductQuantity = listProductQuantity.Where(x => x.ColorId == colorId).ToList();
            }
            return _mapper.Map<List<ProductQuantityViewModel>>(listProductQuantity);
        }

        public List<ProductQuantityViewModel> GetAll(int productId)
        {
            List<ProductQuantity> listProductQuantity = _produtQuantityRepository
               .FindAll(x => x.ProductId == productId, c => c.Color,c=>c.Size).ToList();         
            return _mapper.Map<List<ProductQuantityViewModel>>(listProductQuantity);
        }

        public ColorViewModel GetColorById(int id)
        {
            return _mapper.Map<ColorViewModel>(_colorRepository.FindById(id));
        }

        public List<ColorViewModel> GetColorByProductId(int productId)
        {
            return _mapper.Map<List<ColorViewModel>>(_produtQuantityRepository.FindAll(x => x.ProductId == productId).Select(x => x.Color).ToList());
        }

        public List<ColorViewModel> GetListColor()
        {
            return _mapper.Map<List<ColorViewModel>>(_colorRepository.FindAll().ToList());
        }

        public List<SizeViewModel> GetListSize()
        {
            return _mapper.Map<List<SizeViewModel>>(_sizeRepository.FindAll().ToList());
        }

        public ProductQuantityViewModel GetSingle(int productId, int sizeId, int colorId)
        {
            return _mapper.Map<ProductQuantityViewModel>(_produtQuantityRepository
                .FindAll(x => x.ProductId == productId && x.SizeId == sizeId && x.ColorId == colorId).FirstOrDefault());
        }

        public SizeViewModel GetSizeById(int id)
        {
            return _mapper.Map<SizeViewModel>(_sizeRepository.FindById(id));
        }

        public List<SizeViewModel> GetSizeByProductId(int productId)
        {
            return _mapper.Map<List<SizeViewModel>>(_produtQuantityRepository.FindAll(x => x.ProductId == productId).Select(x => x.Size).ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductQuantityViewModel productQuantityVm)
        {
            _produtQuantityRepository.Update(_mapper.Map<ProductQuantity>(productQuantityVm));
        }

        public void UpdateColor(ColorViewModel color)
        {
            _colorRepository.Update(_mapper.Map<Color>(color));
        }

        public void UpdateSize(SizeViewModel size)
        {
            _sizeRepository.Update(_mapper.Map<Size>(size));
        }
    }
}
