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

        List<SlideViewModel> listSlide { get; set; }

        List<AdvertistmentViewModel> listAdvertistmentTop { get; set; }

        List<AdvertistmentViewModel> listAdvertistmentBottop { set; get; }

        List<ProductViewModel> listNewProduct { get; set; }

        List<ProductViewModel> listHotProduct { get; set; }

        List<ProductViewModel> listPromotionProduct { get; set; }

        List<BlogViewModel> listBlog { get; set; }
    }
}
