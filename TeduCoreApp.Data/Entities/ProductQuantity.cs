using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductQuantities")]
    public class ProductQuantity : DomainEntity<int>
    {
        public ProductQuantity()
        {

        }
        public ProductQuantity(ProductQuantityViewModel productQuantityVm)
        {
            ProductId = productQuantityVm.ProductId;
            SizeId = productQuantityVm.SizeId;
            ColorId = productQuantityVm.ColorId;
            Quantity = productQuantityVm.Quantity;
        }

        [Column(Order = 1)]
        public int ProductId { get; set; }

        [Column(Order = 2)]
        public int SizeId { get; set; }


        [Column(Order = 3)]
        public int ColorId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("SizeId")]
        public virtual Size Size { get; set; }

        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }
    }
}
