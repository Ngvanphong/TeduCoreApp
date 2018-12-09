using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Application.Interfaces
{
   public interface IWholePriceService:IDisposable
    {
        void Add(WholePriceViewModel wholePriceVm);

        void Update(WholePriceViewModel wholePriceVm);

        List<WholePriceViewModel> GetAllByProductId(int productId);

        void Delete(int id);

        void SaveChanges();
    }
}
