using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;


namespace TeduCoreApp.Data.ViewModels.Permission
{
   public class PermissionViewModel
    {
        public Guid RoleId { get; set; }


        public string FunctionId { get; set; }

        public bool CanCreate { set; get; }
        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }
        public bool CanDelete { set; get; }

        public virtual AppRole AppRole { get; set; }


        public virtual Function Function { get; set; }

    }
}
