using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeduCoreApp.Data.ViewModels.Product
{
   public class ProductImageViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        
        public virtual ProductViewModel Product { get; set; }

        [StringLength(250)]
        public string Path { get; set; }

        [StringLength(250)]
        public string Caption { get; set; }
    }
}
