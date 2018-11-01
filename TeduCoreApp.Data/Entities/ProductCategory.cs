using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductCategories")]
    public class ProductCategory : DomainEntity<int>,
     IHasSeoMetaData, ISwitchable, ISortTable, IDateTracking
    {
        public ProductCategory()
        {
            Products = new List<Product>();
        }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }

        public int? ParentId { get; set; }

        public int? HomeOrder { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
        [MaxLength(255)]
        public string SeoPageTitle { set; get; }
        [MaxLength(255)]
        [Column(TypeName ="varchar")]
        public string SeoAlias { set; get; }
        [MaxLength(255)]
        public string SeoKeywords { set; get; }
        [MaxLength(255)]
        public string SeoDescription { set; get; }

        public virtual ICollection<Product> Products { set; get; }
    }
}