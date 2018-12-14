using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Advertistments")]
    public class Advertistment : DomainEntity<int>, ISwitchable, ISortTable
    {
        public Advertistment()
        {

        }
        public Advertistment(AdvertistmentViewModel advertistmentVm)
        {
            Name = advertistmentVm.Name;
            Description = advertistmentVm.Description;
            Image = advertistmentVm.Image;
            Url = advertistmentVm.Url;
            PositionId = advertistmentVm.PositionId;
            PageId = advertistmentVm.PageId;
            Status = advertistmentVm.Status;
            DateModified = DateTime.Now;
            SortOrder = advertistmentVm.SortOrder;
        }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(20)]
        public string PositionId { get; set; }

        [MaxLength(20)]
        public string PageId { get; set; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }

        [ForeignKey("PositionId")]
        public virtual AdvertistmentPosition AdvertistmentPosition { get; set; }

        [ForeignKey("PageId")]
        public virtual AdvertistmentPage AdvertistmentPage { get; set; }
    }
}
