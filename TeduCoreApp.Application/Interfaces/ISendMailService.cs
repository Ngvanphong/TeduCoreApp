using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeduCoreApp.Application.Interfaces
{
   public interface ISendMailService
    {
        Task SendMultiEmail(List<string> listEmail, string subject, string message, string token);
    }
}
