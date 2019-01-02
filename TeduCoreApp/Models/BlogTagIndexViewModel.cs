using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.Data.ViewModels.Tag;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Models
{
    public class BlogTagIndexViewModel
    {
        public BlogTagIndexViewModel()
        {
            ResultPagging = new WebResultPaging<BlogViewModel> { };
        }
        public WebResultPaging<BlogViewModel> ResultPagging { get; set; }

        public List<AdvertistmentViewModel> Advertistments { get; set; }

        public List<TagViewModel> Tags { get; set; }

        public string DomainApi { get; set; }
    }
}
