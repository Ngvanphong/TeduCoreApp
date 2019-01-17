using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Models;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Controllers.Components
{
    public class FooterViewComponent : ViewComponent
    {
        private IProductCategoryService _productCategoryService;
        private IContactService _contactService;
        private IPantnerService _pantnerService;
        private IConfiguration _config;
        private IMemoryCache _cache;

        public FooterViewComponent(IProductCategoryService productCategoryService, IContactService contactService,
            IPantnerService pantnerService, IConfiguration config, IMemoryCache cache)
        {
            _productCategoryService = productCategoryService;
            _contactService = contactService;
            _pantnerService = pantnerService;            
            _config = config;
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerCache = _cache.GetOrCreate(CacheKeys.Footer, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(60);
                FooterViewModel footer = new FooterViewModel() { };
                footer.Contacts = _contactService.GetContact();
                footer.ProductCategorys = _productCategoryService.GetCategoryFooter(6);
                footer.Pantners = _pantnerService.GetAllStatus();
                footer.DomainApi = _config["DomainApi:Domain"];
                return footer;
            });
           
            return View(footerCache);
        }

        
    }
}