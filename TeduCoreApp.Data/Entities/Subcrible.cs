using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.Subcrible;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Subcribles")]
    public class Subcrible: DomainEntity<int>
    {
        public Subcrible()
        {

        }
        public Subcrible(SubcribleViewModel subcribleVm)
        {
            Email = subcribleVm.Email;
        }
        [MaxLength(255)]
        public string Email { get; set; }
    }
}
