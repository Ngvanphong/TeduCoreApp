using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeduCoreApp.WebApi.ViewModel
{
    public class SendEmailViewModel
    {
        public List<string> ListEmail { set;get; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
