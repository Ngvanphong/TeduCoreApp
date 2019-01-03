using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.ViewModels.Contact;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ContactDetails")]
    public class Contact : DomainEntity<string>
    {
        public Contact()
        {

        }

        public Contact(ContactViewModel contactVm)
        {
            Name = contactVm.Name;
            Phone = contactVm.Phone;
            Email = contactVm.Email;
            Website = contactVm.Website;
            Address = contactVm.Address;
            Other = contactVm.Other;
            Lat = contactVm.Lat;
            Lng = contactVm.Lng;
            Status = contactVm.Status;
        }
        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(50)]
        public string Phone { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(250)]
        public string Website { set; get; }

        [StringLength(250)]
        public string Address { set; get; }

        public string Other { set; get; }

        public double? Lat { set; get; }

        public double? Lng { set; get; }

        public Status Status { set; get; }
    }
}
