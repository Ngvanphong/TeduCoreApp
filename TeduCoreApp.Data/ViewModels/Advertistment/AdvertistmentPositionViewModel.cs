using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeduCoreApp.Data.ViewModels.Advertistment
{
    public class AdvertistmentPositionViewModel
    {
        public string Id { set; get; }      

        [StringLength(250)]
        public string Name { get; set; }
    }
}
