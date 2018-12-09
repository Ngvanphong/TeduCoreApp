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
    public class WholePriceService : IWholePriceService
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private IRepository<WholePrice, int> _wholePriceRepository;
        public WholePriceService(IMapper mapper, IUnitOfWork unitOfWork, IRepository<WholePrice, int> wholePriceRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _wholePriceRepository = wholePriceRepository;
        }

        public void Add(WholePriceViewModel wholePriceVm)
        {
            _wholePriceRepository.Add(_mapper.Map<WholePrice>(wholePriceVm));
        }

        public void Delete(int id)
        {
            _wholePriceRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<WholePriceViewModel> GetAllByProductId(int productId)
        {
            return _mapper.Map<List<WholePriceViewModel>>(_wholePriceRepository.FindAll(x => x.ProductId == productId).OrderBy(x=>x.FromQuantity).ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(WholePriceViewModel wholePriceVm)
        {
            _wholePriceRepository.Update(_mapper.Map<WholePrice>(wholePriceVm));
        }
    }
}
