using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.Data.ViewModels.Tag;

namespace TeduCoreApp.Models
{
    public class BlogDetailIndexViewModel
    {
        public BlogViewModel Bog { get; set; }

        public List<TagViewModel> TagsForBlogDetail { get; set; }

        public List<AdvertistmentViewModel> Advertistments { get; set; }

        public List<TagViewModel> Tags { get; set; }

        public string DomainApi { get; set; }
    }
}
