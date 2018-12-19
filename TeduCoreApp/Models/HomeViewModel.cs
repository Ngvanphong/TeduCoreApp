using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Data.ViewModels.Slide;

namespace TeduCoreApp.Models
{
    public class HomeViewModel
    {
    public List<SlideViewModel> ListSlide { get; set; }

    public List<AdvertistmentViewModel> ListAdvertistmentTop { get; set; }
        
    public List<AdvertistmentViewModel> ListAdvertistmentBottom { set; get; }

    public List<ProductViewModel> ListNewProduct { get; set; }
  
    public List<ProductViewModel> ListHotProduct { get; set; }

    public List<ProductViewModel> ListPromotionProduct { get; set; }
   
    public List<BlogViewModel> ListBlog { get; set; }

    public string DomainApi { get; set; }

    }
}
