using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Advertistment;

namespace TeduCoreApp.Application.Interfaces
{
   public interface IAdvertistmentService:IDisposable
    {
        void Add(AdvertistmentViewModel advertistmentVm);

        void UpdateDb(Advertistment advertistmentDb);

        void Delete(int id);

        List<AdvertistmentViewModel> GetAll(int page, int pageSize, string filter, out int totalRow);

        AdvertistmentViewModel Detail(int id);

        List<AdvertistmentViewModel> GetbyPageAndPosition(string pageId, string positonId);

        void SaveChanges();

        void AddPageName(AdvertistmentPageViewModel advertistmentPageVm);

        void DeletePageName(string id);

        void AddPosition(AdvertistmentPositionViewModel advertistmentPositionVm);

      

    }
}
