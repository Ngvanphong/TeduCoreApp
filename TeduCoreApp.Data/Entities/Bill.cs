using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Bills")]
    public class Bill : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public Bill() { }

        public Bill(BillViewModel billVm)
        {
            CustomerName = billVm.CustomerName;
            CustomerAddress = billVm.CustomerAddress;
            CustomerMobile = billVm.CustomerMobile;
            CustomerMessage = billVm.CustomerMessage;
            BillStatus = billVm.BillStatus;
            PaymentMethod = billVm.PaymentMethod;
            Status = billVm.Status;
            CustomerId = billVm.CustomerId;
            CustomerEmail = billVm.CustomerEmail;
        }
    
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

        [DefaultValue(Status.Active)]
        public Status Status { set; get; } = Status.Active;
   
        public Guid? CustomerId { set; get; }

        [ForeignKey("CustomerId")]
        public virtual AppUser User { set; get; }

        public virtual ICollection<BillDetail> BillDetails { set; get; }
    }
}
