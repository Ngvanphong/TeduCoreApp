using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.ViewModels.FunctionVm;


namespace TeduCoreApp.Application.Interfaces
{
   public interface IFunctionService
    {
        FunctionViewModel Create(FunctionViewModel functionVm);

        List<FunctionViewModel> GetAll(string filter);

        List<FunctionViewModel> GetAllWithPermission(string userId);

        List<FunctionViewModel> GetAllWithParentID(string parentId);

        FunctionViewModel Get(string id);

        void Update(FunctionViewModel functionVm);

        void Delete(string id);

        void SaveChange();

        bool CheckExistedId(string id);
    }
}
