using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("WholePrices")]
    public class WholePrice : DomainEntity<int>
    {
        public WholePrice()
        {

        }

        public WholePrice(WholePriceViewModel wholePriceVm)
        {
            ProductId = wholePriceVm.ProductId;
            FromQuantity = wholePriceVm.FromQuantity;
            ToQuantity = wholePriceVm.ToQuantity;
            Price = wholePriceVm.Price;
        }

        public int ProductId { get; set; }

        public int FromQuantity { get; set; }

        public int ToQuantity { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
