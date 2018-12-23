using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Data.ViewModels.Tag;

namespace TeduCoreApp.Models
{
    public class ProductDetailViewModel
    {
        public ProductViewModel ProductDetail { get; set; }

        public List<ProductViewModel> ProductRelate { get; set; }

        public List<ProductViewModel> ProductUpsell { get; set; }

        public List<TagViewModel> ProductTags { set; get; }

        public List<ColorViewModel> Colors { get; set; }

        public List<SizeViewModel> Sizes { get; set; }

        public List<WholePriceViewModel> WholePrices { get; set; }

        public string DomainApi { get; set; }

        public List<ProductImageViewModel> ProductImages { get; set; }
     
    }
}
