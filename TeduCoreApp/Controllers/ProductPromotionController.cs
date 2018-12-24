using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Models;
using TeduCoreApp.Utilities.Dtos;
using static TeduCoreApp.Utilities.Constants.CommonConstants;

namespace TeduCoreApp.Controllers
{
    public class ProductPromotionController : Controller
    {
        private IProductService _productService;
        private ISlideService _slideService;
        private IAdvertistmentService _advertistmentService;
        private IConfiguration _config;
        public ProductPromotionController(IProductService productService, ISlideService slideService,
            IAdvertistmentService advertistmentService, IConfiguration config)
        {
            _productService = productService;
            _slideService = slideService;
            _advertistmentService = advertistmentService;
            _config = config;
        }
        [Route("promotionPrice.html")]
        public IActionResult Index()
        {
            ProductPromotionIndexViewModel promotion = new ProductPromotionIndexViewModel() { };
            promotion.Slides = _slideService.GetAll(true);
            promotion.Advertistments = _advertistmentService.GetbyPageAndPosition(PageName.Orther, PositionName.Default);
            promotion.DomainApi = _config["DomainApi:Domain"];
            promotion.Tags = _productService.GetAllTag(15);
            promotion.ProductCategorys = _productService.GetListCategoryHasPromotion();
            return View(promotion);
        }

       [Route("productPromotion/getall")]
        [HttpGet]
        public IActionResult GetProduct(int? category,string sort, int page, int pageSize)
        {
            List<ProductViewModel> products = _productService.GetAllPromotionProductByCatygory(category, sort, page, pageSize, out int totalRows);
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