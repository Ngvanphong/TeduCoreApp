using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Models
{
    public class WishlistViewModel
    {
        public int ProductId { set; get; }

        public ProductViewModel ProductVm { get; set; }
               
    }
}
