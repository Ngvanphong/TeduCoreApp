using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Sizes")]
    public class Size : DomainEntity<int>
    {
        public Size()
        {

        }
        public Size(SizeViewModel sizeVm)
        {
            Name = sizeVm.Name;
        }

        [StringLength(250)]
        public string Name
        {
            get; set;
        }
    }
}
