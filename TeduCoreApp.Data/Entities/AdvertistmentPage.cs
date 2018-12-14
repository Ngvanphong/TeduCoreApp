using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("AdvertistmentPages")]
    public class AdvertistmentPage : DomainEntity<string>
    {
        public AdvertistmentPage()
        {

        }

        public AdvertistmentPage(AdvertistmentPageViewModel advertistmentPageVm)
        {
            Id = advertistmentPageVm.Id;
            Name = advertistmentPageVm.Name;
        }

        public string Name { get; set; }

        public virtual ICollection<Advertistment> Advertistments { get; set; }
    }
}
