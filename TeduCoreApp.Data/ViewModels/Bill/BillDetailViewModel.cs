using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Data.ViewModels.Bill
{
   public class BillDetailViewModel
    {
        public int Id { get; set; }

        public int BillId { set; get; }

        public int ProductId { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

    }
}
