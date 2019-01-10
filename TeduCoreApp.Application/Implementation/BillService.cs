using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class BillService : IBillService
    {
        private IRepository<Bill, int> _billRepository;
        private IRepository<BillDetail, int> _billDetailRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BillService(IRepository<Bill, int> billRepository, IRepository<BillDetail, int> billDetailRepository,
           IUnitOfWork unitOfWork, IMapper mapper)
        {
            _billRepository = billRepository;
            _billDetailRepository = billDetailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public int Add(BillViewModel billVm)
        {
            Bill bill = new Bill();
            bill = _mapper.Map<Bill>(billVm);
            _billRepository.Add(bill);
            _unitOfWork.Commit();
            return bill.Id;
        }

        public void AddBillDetail(BillDetailViewModel billDetail)
        {
            _billDetailRepository.Add(_mapper.Map<BillDetail>(billDetail));
        }

        public void DeleteBill(int id)
        {
            _billRepository.Remove(id);
        }

        public void DeleteBillDetail(int id)
        {
            _billDetailRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<BillDetailViewModel> GetBillDetails(int billId)
        {
            return _mapper.Map<List<BillDetailViewModel>>(_billDetailRepository
                .FindAll(x => x.BillId == billId,x=>x.Product,x=>x.Size,x=>x.Color).ToList());
        }

        public BillViewModel GetDetail(int id)
        {
            return _mapper.Map<BillViewModel>(_billRepository.FindById(id));
        }

        public List<BillViewModel> GetList(string startDate, string endDate, string customerName,BillStatus? billStatus, 
            int pageIndex,int pageSize, out int totalRow)
        {

            var query = _billRepository.FindAll();
            if (!string.IsNullOrEmpty(startDate))
            {
                startDate = startDate.Substring(0, 24);
                DateTime dateStart = DateTime.ParseExact(startDate, "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                query = query.Where(x => x.DateCreated >= dateStart);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                endDate = endDate.Substring(0, 24);
                DateTime dateEnd = DateTime.ParseExact(endDate, "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                query = query.Where(x => x.DateCreated <= dateEnd);
            }
            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(x => x.CustomerName.Contains(customerName));
            }
            if (billStatus.HasValue&& billStatus!=BillStatus.All)
            {
                query = query.Where(x => x.BillStatus==billStatus);
            }
            totalRow = query.Count();
            query= query.OrderByDescending(x => x.DateCreated).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return _mapper.Map<List<BillViewModel>>(query.ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(BillViewModel billVm)
        {
            _billRepository.Update(_mapper.Map<Bill>(billVm));
        }
    }
}