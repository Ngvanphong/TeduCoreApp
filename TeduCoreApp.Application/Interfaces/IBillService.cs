using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.ViewModels.Bill;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IBillService:IDisposable
    {
        void Add (BillViewModel billVm);

        void Update(BillViewModel billVm);

        List<BillViewModel> GetList(string startDate, string endDate, string customerName, BillStatus? billStatus,
            int pageIndex, int pageSize, out int totalRow);

        BillViewModel GetDetail(int id);

        List<BillDetailViewModel> GetBillDetails(int billId);

        void AddBillDetail(BillDetailViewModel billDetail);

        void DeleteBillDetail(int id);

        void DeleteBill(int id);

        void SaveChanges();
    }
}
