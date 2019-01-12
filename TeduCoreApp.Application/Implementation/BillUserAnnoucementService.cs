using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Data.ViewModels.BillUserAnnoucement;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class BillUserAnnoucementService : IBillUserAnnoucementService
    {
        private IRepository<BillUserAnnoucement, int> _billUserAnnoucementRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IRepository<Bill,int> _billRepository;

        public BillUserAnnoucementService(IRepository<BillUserAnnoucement, int> billUserAnnoucementRepository, IUnitOfWork unitOfWork,
           IMapper mapper, IRepository<Bill, int> billRepository)
        {
            _billUserAnnoucementRepository = billUserAnnoucementRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _billRepository = billRepository;
        }

        public void Add(BillUserAnnoucementViewModel billUserAnnoucementVm)
        {
            _billUserAnnoucementRepository.Add(_mapper.Map<BillUserAnnoucement>(billUserAnnoucementVm));
        }

        public void AddDb(BillUserAnnoucement billUserAnnoucement)
        {
            _billUserAnnoucementRepository.Add(billUserAnnoucement);
        }

        public void Delete(int id)
        {
            _billUserAnnoucementRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<BillUserAnnoucementViewModel> GetAll()
        {
           return _mapper.Map<List<BillUserAnnoucementViewModel>>(_billUserAnnoucementRepository.FindAll().ToList());
        }

        public List<BillUserAnnoucementViewModel> GetAllByHasRead()
        {
            return _mapper.Map<List<BillUserAnnoucementViewModel>>(_billUserAnnoucementRepository.FindAll(x=>x.HasRead==false).ToList());
        }

        public BillUserAnnoucementViewModel GetById(int id)
        {
            return _mapper.Map<BillUserAnnoucementViewModel>(_billUserAnnoucementRepository.FindById(id));
        }

        public BillUserAnnoucement GetByUserBill(int id, Guid userId)
        {
            return _mapper.Map<BillUserAnnoucement>(_billUserAnnoucementRepository.FindSingle(x=>x.BillId==id&&x.UserId==userId));
        }

        public List<BillViewModel> ListAllUnread(string userId)
        {
            var ListBillinAnnoucement = _billUserAnnoucementRepository.FindAll(x => x.UserId.ToString() == userId).Select(x => x.BillId);
            var ListOrder = _billRepository.FindAll().Select(x => x.Id);
            List<int> ListOrdreCanRead = ListOrder.Except(ListBillinAnnoucement).ToList();
            List<Bill> query = new List<Bill>
            {
            };
            foreach (var item in ListOrdreCanRead)
            {
                Bill bill = _billRepository.FindById(item);
                query.Add(bill);
            }           
            return _mapper.Map<List<BillViewModel>>(query);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateDb(BillUserAnnoucement billUserAnnoucement)
        {
            _billUserAnnoucementRepository.Update(billUserAnnoucement);
        }
    }
}