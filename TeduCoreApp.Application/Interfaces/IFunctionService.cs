﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.FunctionVm;


namespace TeduCoreApp.Application.Interfaces
{
   public interface IFunctionService:IDisposable
    {
        void Create(FunctionViewModel functionVm);

        List<FunctionViewModel> GetAll(string filter);

        List<FunctionViewModel> GetAllWithPermission(string userId);

        List<FunctionViewModel> GetAllWithParentId(string parentId);

        FunctionViewModel Get(string id);

        void Add(FunctionViewModel functionVm);

        void Update(FunctionViewModel functionVm);

        void Delete(string id);

        void SaveChanges();

        bool CheckExistedId(string id);

        Task<bool> CheckPermission(string functionId, string action, string[] roles);
    }
}
