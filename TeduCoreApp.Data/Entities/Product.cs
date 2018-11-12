using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Products")]
    public class Product : DomainEntity<int>, IHasSeoMetaData, ISwitchable, IDateTracking
    {
        public Product()
        {

        }
        public Product(ProductViewModel productVm)
        {
            Name = productVm.Name;
            CategoryId = productVm.CategoryId;
            Image = productVm.Image;
            Price = productVm.Price;
            OriginalPrice = productVm.OriginalPrice;
            PromotionPrice = productVm.PromotionPrice;
            Description = productVm.Description;
            Content = productVm.Content;
            HomeFlag = productVm.HomeFlag;
            HotFlag = productVm.HotFlag;
            Tag = productVm.Tag;
            Unit = productVm.Unit;
            Status = productVm.Status;
            SeoPageTitle = productVm.SeoPageTitle;
            SeoAlias = productVm.SeoAlias;
            SeoKeywords = productVm.SeoKeywords;
            SeoDescription = productVm.SeoDescription;           
        }
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
        public decimal OriginalPrice { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public string Content { get; set; }

        public bool? HomeFlag { get; set; }

        public bool? HotFlag { get; set; }

        public int ViewCount { get; set; }
        [MaxLength(255)]
        public string Tag { set; get; }
        [MaxLength(255)]
        public string Unit { get; set; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public Status Status { set; get; }
        [MaxLength(255)]
        public string SeoPageTitle { set; get; }
        [Column(TypeName ="varchar(255)")]
        public string SeoAlias { set; get; }
        [MaxLength(255)]
        public string SeoKeywords { set; get; }
        [MaxLength(255)]
        public string SeoDescription { set; get; }
    }
}
