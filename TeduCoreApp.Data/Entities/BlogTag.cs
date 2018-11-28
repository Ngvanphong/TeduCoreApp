using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("BlogTags")]
    public class BlogTag : DomainEntity<int>
    {
        public BlogTag()
        {

        }
        public BlogTag(BlogTagViewModel blogTagVm)
        {
            BlogId = blogTagVm.BlogId;
            TagId = blogTagVm.TagId;
        }

        public int BlogId { set; get; }

      
        [Column(TypeName ="varchar(50)")]
        public string TagId { set; get; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}
