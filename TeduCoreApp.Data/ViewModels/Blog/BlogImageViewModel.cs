using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeduCoreApp.Data.ViewModels.Blog
{
   public class BlogImageViewModel
    {
        public int Id { get; set; }

        public int BlogId { get; set; }
  
        [StringLength(250)]
        public string Path { get; set; }

        [StringLength(250)]
        public string Caption { get; set; }
    }
}
