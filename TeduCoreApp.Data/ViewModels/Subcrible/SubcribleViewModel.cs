using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeduCoreApp.Data.ViewModels.Subcrible
{
   public class SubcribleViewModel
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }
    }
}
