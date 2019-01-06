using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.ViewModels.Pantner;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Pantners")]
    public class Pantner : DomainEntity<int>
    {
        public Pantner()
        {

        }
        public Pantner(PantnerViewModel pantnerVm)
        {
            Name = pantnerVm.Name;
            Image = pantnerVm.Image;
            Url = pantnerVm.Url;
            Status = pantnerVm.Status;
        }
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        public Status Status { set; get; }
    }
}