using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductTags")]
   public class ProductTag:DomainEntity<int>
    {
    
        public int ProductId { set; get; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
      
        [Column(TypeName ="varchar(50)")]
        public string TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
