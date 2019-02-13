﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Data.ViewModels.Bill
{
   public class BillViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { set; get; }

        [MaxLength(256)]
        public string CustomerEmail { set; get; }

        [MaxLength(256)]
        public string CustomerMessage { set; get; }

        public PaymentMethod PaymentMethod { set; get; }

        public BillStatus BillStatus { set; get; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public Status Status { set; get; } 

        public Guid? CustomerId { set; get; }

   
        public decimal? FeeShipping { get; set; }

  
        public decimal? TotalMoneyOrder { get; set; }

   
        public decimal? TotalMoneyPayment { get; set; }

   
        public decimal? TotalOriginalPrice { get; set; }

        public decimal? BalanceForBill { get; set; }

        public virtual List<BillDetailViewModel> BillDetails { set; get; }
    }
}
