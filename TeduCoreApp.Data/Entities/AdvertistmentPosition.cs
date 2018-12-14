using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{

    [Table("AdvertistmentPositions")]
    public class AdvertistmentPosition : DomainEntity<string>
    {
        public AdvertistmentPosition()
        {

        }
        public AdvertistmentPosition(AdvertistmentPositionViewModel advertistmentPositionVm)
        {
            Id = advertistmentPositionVm.Id;          
            Name = advertistmentPositionVm.Name;
        }
        

        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<Advertistment> Advertistments { get; set; }
    }
}
