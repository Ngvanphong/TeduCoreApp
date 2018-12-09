using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{

    [Table("Colors")]
    public class Color : DomainEntity<int>
    {
       
        public Color()
        {

        }
        public Color(ColorViewModel colorVm)
        {
            Name = colorVm.Name;
            Code = colorVm.Code;
        }
        [StringLength(250)]
        public string Name { get; set; }
    
        [StringLength(250)]
        public string Code { get; set; }
    }
}
