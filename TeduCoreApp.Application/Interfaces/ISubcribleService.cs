using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.ViewModels.Subcrible;

namespace TeduCoreApp.Application.Interfaces
{
  public interface ISubcribleService:IDisposable
    {
        void Add(string email);

        void SaveChanges();

        bool CheckExit(string email);

        List<SubcribleViewModel> GetAll();

        void Delete(int id);
    }
}
