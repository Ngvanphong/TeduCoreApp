using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeduCoreApp.WebApi.Extensions
{
    public static class DeleteElement
    {
        public static void DeletementByString(this string path , IHostingEnvironment env)
        {
            string webHost = env.WebRootPath;
            string fullPath = webHost + path;
            System.IO.File.Delete(fullPath);
        }
    }
}
