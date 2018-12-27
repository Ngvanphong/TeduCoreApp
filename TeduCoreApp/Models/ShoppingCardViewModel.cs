using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Product;


namespace TeduCoreApp.Models
{
    public class ShoppingCardViewModel
    {
        public int ProductId { set; get; }

        public ProductViewModel ProductVm { get; set; }

        public ColorViewModel ColorVm { get; set; }

        public SizeViewModel SizeVm { get; set; }

        public int Quantity { get; set; }

      
    }
}
