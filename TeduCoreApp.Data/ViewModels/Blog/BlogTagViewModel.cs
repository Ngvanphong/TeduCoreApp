using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.Tag;

namespace TeduCoreApp.Data.ViewModels.Blog
{
   public class BlogTagViewModel
    {
        public int Id { get; set; }
        public int BlogId { set; get; }

        [Column(TypeName = "varchar(50)")]
        public string TagId { set; get; }
     
    }
}
