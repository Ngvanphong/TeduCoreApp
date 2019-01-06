using System.Collections.Generic;
using TeduCoreApp.Data.ViewModels.Contact;
using TeduCoreApp.Data.ViewModels.Pantner;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.Models
{
    public class FooterViewModel
    {
        public List<ProductCategoryViewModel> ProductCategorys { get; set; }

        public ContactViewModel Contacts { set; get; }

        public List<PantnerViewModel> Pantners { get; set; }

        public string DomainApi { get; set; }
    }
}