using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.BillUserAnnoucement;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("BillUserAnnoucements")]
    public class BillUserAnnoucement: DomainEntity<int>
    {
        public BillUserAnnoucement()
        {

        }
        public BillUserAnnoucement(BillUserAnnoucementViewModel billUserAnnoucementVm)
        {
            BillId = billUserAnnoucementVm.BillId;
            UserId = billUserAnnoucementVm.UserId;
            HasRead = billUserAnnoucementVm.HasRead;
        }
        public int BillId { get; set; }

        public Guid UserId { get; set; }

        public bool HasRead { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }
    }
}
