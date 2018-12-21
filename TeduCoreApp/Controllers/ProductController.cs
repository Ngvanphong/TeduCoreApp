using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Models;
using TeduCoreApp.Utilities.Dtos;
using static TeduCoreApp.Utilities.Constants.CommonConstants;

namespace TeduCoreApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ISlideService _slideService;
        private IAdvertistmentService _advertistmentService;
        private IConfiguration _config;
        private IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, ISlideService slideService, IAdvertistmentService advertistmentService,
            IConfiguration config,IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _slideService = slideService;
            _advertistmentService = advertistmentService;
            _config = config;
            _productCategoryService = productCategoryService;
        }

        [Route("{alias}.pc-{id}.html")] 
        public IActionResult Index(int id)
        {
            ProductIndexViewModel product = new ProductIndexViewModel() { };
            product.ProductCategory = _productCategoryService.GetById(id);
            product.Slides = _slideService.GetAll(true);
            product.Advertistments = _advertistmentService.GetbyPageAndPosition(PageName.Orther, PositionName.Default);
            product.Tags = _productService.GetAllTag(15);
            product.DomainApi = _config["DomainApi:Domain"];
            return View(product);
        }

        [Route("product/getProductByCategory")]
        [HttpGet]
        public IActionResult GetProduct(int id,string sort,int page,int pageSize)
        {
            List<ProductViewModel> products = _productService.GetAllByCategoryPaging(id, page, pageSize, sort, out int totalRows);
            return new OkObjectResult(new WebResultPaging<ProductViewModel>()
            {              
                Items = products,
                PageIndex = page,
                PageSize = pageSize,
                TotalRows = totalRows,
            });
        }

    }
}