using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Data.ViewModels.Identity
{
   public class AppRoleViewModel
    {
        public AppRoleViewModel()
        {

        }
     
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
