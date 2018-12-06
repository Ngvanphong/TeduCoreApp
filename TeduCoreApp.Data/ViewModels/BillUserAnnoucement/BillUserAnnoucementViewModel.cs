using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Data.ViewModels.BillUserAnnoucement
{
  public class BillUserAnnoucementViewModel
    {

        public int BillId { get; set; }

        public Guid UserId { get; set; }

        public bool HasRead { get; set; }
   
    }
}
