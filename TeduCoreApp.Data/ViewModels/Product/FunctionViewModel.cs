using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Data.ViewModels.Product
{
   public class FunctionViewModel
    {
        public string Id { set; get; }


        [StringLength(128)]
        public string Name { set; get; }


        [StringLength(250)]
        public string URL { set; get; }


        [StringLength(128)]
        public string ParentId { set; get; }

        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
       
    }
}
