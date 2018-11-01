using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Products")]
    public class Product : DomainEntity<int>, IHasSeoMetaData, ISwitchable, IDateTracking
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }
        [Required]
        public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; }
        [Required]
        public decimal OriginalPricee { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public string Content { get; set; }

        public bool? HomeFlag { get; set; }

        public bool? HotFlag { get; set; }

        public int ViewCount { get; set; }
        [MaxLength(255)]
        public string Tag { set; get; }
        [MaxLength(50)]
        public string Unit { get; set; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

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
    }
}
