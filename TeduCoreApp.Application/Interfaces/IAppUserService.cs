using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IAppUserService:IDisposable
    {
        Task RemoveRolesFromUserCustom(string userId, string[] roles);

    }
}
