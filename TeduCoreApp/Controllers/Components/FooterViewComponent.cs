using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Models;

namespace TeduCoreApp.Controllers.Components
{
    public class FooterViewComponent : ViewComponent
    {
        private IProductCategoryService _productCategoryService;
        private IContactService _contactService;
        private IPantnerService _pantnerService;
        private IConfiguration _config;
       

        public FooterViewComponent(IProductCategoryService productCategoryService, IContactService contactService,
            IPantnerService pantnerService, IConfiguration config)
        {
            _productCategoryService = productCategoryService;
            _contactService = contactService;
            _pantnerService = pantnerService;            
            _config = config;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterViewModel footer = new FooterViewModel() { };
            footer.Contacts = _contactService.GetContact();
            footer.ProductCategorys = _productCategoryService.GetCategoryFooter(6);
            footer.Pantners = _pantnerService.GetAllStatus();
            footer.DomainApi = _config["DomainApi:Domain"];
            return View(footer);
        }

        
    }
}