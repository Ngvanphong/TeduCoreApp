using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Data.ViewModels.Slide;
using TeduCoreApp.Data.ViewModels.Tag;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Models
{
    public class ProductIndexViewModel
    {     
       public List<SlideViewModel> Slides { get; set; }
       public List<AdvertistmentViewModel> Advertistments { get; set; }
       public List<TagViewModel> Tags { get; set; }
       public string DomainApi { get; set; }
       public ProductCategoryViewModel ProductCategory { get; set; }
    }
}
