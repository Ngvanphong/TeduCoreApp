using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Bill;

namespace TeduCoreApp.Models
{
    public class CheckoutViewModel
    {
        public BillViewModel Bills { get; set; }

        public List<BillDetailViewModel> BillDetails { get; set; }
    }
}
