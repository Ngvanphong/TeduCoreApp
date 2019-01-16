using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.SystemConfig;

namespace TeduCoreApp.Application.Interfaces
{
    public interface ISystemConfigService : IDisposable
    {
        List<SystemConfigViewModel> GetAll();

        SystemConfigViewModel Detail(string id);

        SystemConfig DetailDb(string id);
      
        void Delete(string id);

        void Update(SystemConfig systemConfigDb);

        void Add(SystemConfigViewModel systemConfig);

        void SaveChanges();
    }
}
