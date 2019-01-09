using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Tag;

namespace TeduCoreApp.Application.Interfaces
{
   public interface ITagService:IDisposable
    {
        List<TagViewModel> GetAllPagging(int page, int pageSize, out int totalRows,string filter);

        void DeleteMultiNotUse();    
        
        void SaveChanges();
    }
}
