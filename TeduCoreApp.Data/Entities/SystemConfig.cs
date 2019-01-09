using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Data.ViewModels.SystemConfig;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("SystemConfigs")]
    public class SystemConfig : DomainEntity<string>, ISwitchable
    {
        public SystemConfig()
        {

        }
        public SystemConfig(SystemConfigViewModel systemConfigVm)
        {
            Id = systemConfigVm.Id;
            Name = systemConfigVm.Name;
            Value1 = systemConfigVm.Value1;
            Value2 = systemConfigVm.Value2;
            Value3 = systemConfigVm.Value3;
            Value4 = systemConfigVm.Value4;
            Value5 = systemConfigVm.Value5;
            Status = systemConfigVm.Status;
        }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string Value1 { get; set; }

        public int? Value2 { get; set; }

        public bool? Value3 { get; set; }

        public DateTime? Value4 { get; set; }

        public decimal? Value5 { get; set; }

        public Status Status { get; set; }
    }
}
