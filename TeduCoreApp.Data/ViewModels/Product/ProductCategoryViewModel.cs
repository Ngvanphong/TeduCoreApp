using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Data.ViewModels.Product
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }


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

        public string SeoAlias { set; get; }

        [MaxLength(255)]
        public string SeoKeywords { set; get; }

        [MaxLength(255)]
        public string SeoDescription { set; get; }
    }
}
