using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Application.Dapper.ViewModel
{
   public class RevenueReportViewModel
    {
        public DateTime Date { get; set; }

        public decimal Revenue { get; set; }

        public decimal Profit { get; set; }
    }
}
