using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Page;

namespace TeduCoreApp.Application.Interfaces
{
   public interface IPageImageService:IDisposable
    {
        void Add(PageImageViewModel pageImage);      

        PageImageViewModel GetById(int id);
   
        List<PageImageViewModel> GetAllByPageId(int pageId);

        void Delete(int id);

        void SaveChanges();
    }
}
