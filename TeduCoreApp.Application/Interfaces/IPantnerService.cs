using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Pantner;

namespace TeduCoreApp.Application.Interfaces
{
  public  interface IPantnerService:IDisposable
    {
      
        List<PantnerViewModel> GetAll();

        List<PantnerViewModel> GetAllStatus();

        PantnerViewModel GetById(int id);

        Pantner GetByIdDb(int id);

        void Delete(int id);

        void Update(Pantner pantnerDb);

        void Add(PantnerViewModel pantnerVm);

        void SaveChanges();
    }
}
