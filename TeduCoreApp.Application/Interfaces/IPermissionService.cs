using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Permission;

namespace TeduCoreApp.Application.Interfaces
{
   public interface IPermissionService:IDisposable
    {
        List<PermissionViewModel> GetByFunctionId(string functionId);

        List<PermissionViewModel> GetByUserId(Guid userId);

        void Add(PermissionViewModel permission);

        void DeleteAll(string functionId);

        void DeleteAllByRoleId(string roleId);

        void SaveChange();
    }
}
