using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class AdvertistmentService : IAdvertistmentService
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private IRepository<Advertistment, int> _advertistmentRepository;
        private IRepository<AdvertistmentPosition, string> _advertistmentPositionRepostitory;
        private IRepository<AdvertistmentPage, string> _advertistmentPageRepository;

        public AdvertistmentService(IMapper mapper, IUnitOfWork unitOfWork, IRepository<AdvertistmentPage, string> advertistmentPageRepository,
        IRepository<Advertistment, int> advertistmentRepository, IRepository<AdvertistmentPosition, string> advertistmentPositionRepostitory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _advertistmentRepository = advertistmentRepository;
            _advertistmentPositionRepostitory = advertistmentPositionRepostitory;
            _advertistmentPageRepository = advertistmentPageRepository;
        }

        public void Add(AdvertistmentViewModel advertistmentVm)
        {
            _advertistmentRepository.Add(_mapper.Map<Advertistment>(advertistmentVm));
        }

        public void AddPageName(AdvertistmentPageViewModel advertistmentPageVm)
        {
            _advertistmentPageRepository.Add(_mapper.Map<AdvertistmentPage>(advertistmentPageVm));
        }

        public void AddPosition(AdvertistmentPositionViewModel advertistmentPositionVm)
        {
            _advertistmentPositionRepostitory.Add(_mapper.Map<AdvertistmentPosition>(advertistmentPositionVm));
        }

        public void Delete(int id)
        {
            _advertistmentRepository.Remove(id);
        }

        public void DeletePageName(string id)
        {
            _advertistmentPageRepository.Remove(id);
        }

        public AdvertistmentViewModel Detail(int id)
        {
            return _mapper.Map<AdvertistmentViewModel>(_advertistmentRepository.FindById(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<AdvertistmentViewModel> GetAll(int page, int pageSize, string filter, out int totalRow)
        {
            var query = _advertistmentRepository.FindAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }
            totalRow = query.Count();
            query = query.OrderByDescending(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<AdvertistmentViewModel>>(query.ToList());
        }

        public List<AdvertistmentViewModel> GetbyPageAndPosition(string pageId, string positonId)
        {
            var query = _advertistmentRepository.FindAll(x=>x.PageId==pageId&&x.Status==Data.Enums.Status.Active);
                      
            if (!string.IsNullOrEmpty(positonId))
            {
                query = query.Where(x => x.PositionId == positonId);
            }

            return _mapper.Map<List<AdvertistmentViewModel>>(query.ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateDb(Advertistment advertistmentDb)
        {
            _advertistmentRepository.Update(advertistmentDb);
        }
    }
}