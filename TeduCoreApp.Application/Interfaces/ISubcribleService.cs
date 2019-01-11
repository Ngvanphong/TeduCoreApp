using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.ViewModels.Subcrible;

namespace TeduCoreApp.Application.Interfaces
{
  public interface ISubcribleService:IDisposable
    {
        List<SubcribleViewModel> GetPaging(int page, int pageSize,out int totalRow);

        void Add(string email);

        void SaveChanges();

        bool CheckExit(string email);

        List<SubcribleViewModel> GetAll();

        List<string> GetAllEmail();

        void Delete(int id);
    }
}
