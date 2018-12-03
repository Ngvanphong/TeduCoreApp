using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.Slide;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Slides")]
    public class Slide : DomainEntity<int>
    {
        public Slide()
        {

        }

        public Slide(SlideViewModel slideVm)
        {
            Name = slideVm.Name;
            Description = slideVm.Description;
            Image = slideVm.Image;
            Url = slideVm.Url;
            DisplayOrder = slideVm.DisplayOrder;
            Status = slideVm.Status;
            Content = slideVm.Content;
            OrtherPageHome = slideVm.OrtherPageHome;
        }
        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(250)]
        public string Description { set; get; }

        [StringLength(250)]
        [Required]
        public string Image { set; get; }

        [StringLength(250)]
        public string Url { set; get; }

        public int? DisplayOrder { set; get; }

        public bool Status { set; get; }

        public string Content { set; get; }
       
        public bool OrtherPageHome { get; set; }
    }
}
