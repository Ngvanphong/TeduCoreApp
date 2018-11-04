using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Permission;

namespace TeduCoreApp.Application.Interfaces
{
   public interface IPermissionService
    {
        ICollection<PermissionViewModel> GetByFunctionId(string functionId);

        ICollection<PermissionViewModel> GetByUserId(Guid userId);

        void Add(PermissionViewModel permission);

        void DeleteAll(string functionId);

        void DeleteAllByRoleID(string roleID);

        void SaveChange();
    }
}
