using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.ViewModels.Page;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("PageImages")]
    public class PageImage : DomainEntity<int>
    {
        public PageImage()
        {
        }

        public PageImage(PageImageViewModel pageImageVm)
        {
            PageId = pageImageVm.PageId;
            Path = pageImageVm.Path;
            Caption = pageImageVm.Caption;
        }
        
        public int PageId { get; set; }

        [ForeignKey("PageId")]
        public virtual Page Page { get; set; }

        [StringLength(255)]
        public string Path { get; set; }

        [StringLength(250)]
        public string Caption { get; set; }
    }
}