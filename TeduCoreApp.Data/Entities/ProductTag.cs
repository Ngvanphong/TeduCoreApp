using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductTag")]
   public class ProductTag
    {
        [Key]
        [Column(Order =1)]
        public int ProductId { set; get; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [Key]
        [StringLength(50)]
        [Column(TypeName ="varchar",Order =2)]
        public string TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
