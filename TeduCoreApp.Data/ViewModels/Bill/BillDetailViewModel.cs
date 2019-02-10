﻿using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.ViewModels.Product;

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

        public decimal OriginalPrice { get; set; }

        public virtual ProductViewModel Product { set; get; }

        public virtual ColorViewModel Color { set; get; }

        public virtual SizeViewModel Size { set; get; }

    }
}
