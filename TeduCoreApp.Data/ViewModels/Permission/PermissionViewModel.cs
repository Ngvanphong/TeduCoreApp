using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.Data.ViewModels.Identity;

namespace TeduCoreApp.Data.ViewModels.Permission
{
   public class PermissionViewModel
    {
        public int Id { get; set; }

        public  Guid RoleId { get; set; }

        public string FunctionId { get; set; }

        public bool CanCreate { set; get; }

        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }

        public bool CanDelete { set; get; }

        public virtual AppRoleViewModel AppRole { get; set; }


        public virtual FunctionViewModel Function { get; set; }

    }
}
