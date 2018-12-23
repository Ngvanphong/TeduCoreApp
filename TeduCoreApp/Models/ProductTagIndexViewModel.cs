using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Data.ViewModels.Slide;
using TeduCoreApp.Data.ViewModels.Tag;

namespace TeduCoreApp.Models
{
    public class ProductTagIndexViewModel
    {
        public List<SlideViewModel> Slides { get; set; }
        public List<AdvertistmentViewModel> Advertistments { get; set; }
        public string DomainApi { get; set; }
        public TagViewModel ProductTag { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}
