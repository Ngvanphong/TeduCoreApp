using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Controllers.Components
{
    public class MainNavigationViewComponent:ViewComponent
    {
        private IProductCategoryService _productCategoryService;
        private IMemoryCache _cache;
        public MainNavigationViewComponent(IProductCategoryService productCategoryService, IMemoryCache cache)
        {
            _productCategoryService = productCategoryService;
            _cache = cache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = _cache.GetOrCreate(CacheKeys.CategoryMain, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(60);
                return _productCategoryService.GetAll();
            });
            return View(category);
        }
    }
}
