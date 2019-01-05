using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Data.ViewModels.Page;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Pages")]
    public class Page : DomainEntity<int>, ISwitchable
    {
        public Page()
        {

        }
        public Page(PageViewModel pageVm)
        {
            Name = pageVm.Name;
            Alias = pageVm.Alias;
            Content = pageVm.Content;
            Status = pageVm.Status;
        }
        [Required]
        [MaxLength(255)]
        public string Name { set; get; }

        [MaxLength(256)]
        [Required]
        public string Alias { set; get; }

        public string Content { set; get; }

        public Status Status { set; get; }
    }
}
