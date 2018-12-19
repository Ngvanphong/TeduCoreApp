using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Slide;

namespace TeduCoreApp.Application.Interfaces
{
   public interface ISlideService:IDisposable
    {
        List<SlideViewModel> GetAllPagging(int page,int pageSize, string filter, out int totalRow);

        List<SlideViewModel> GetAll(bool ortherPageHome);

        SlideViewModel GetById(int id);

        Slide GetByIdDb(int id);

        void Delete(int id);

        void Update(Slide slideDb);

        void Add(SlideViewModel slideVm);

        void SaveChanges();
    }
}
