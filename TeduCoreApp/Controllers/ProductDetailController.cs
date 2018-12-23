using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Models;

namespace TeduCoreApp.Controllers
{
    public class ProductDetailController : Controller
    {
        private IProductService _productService;
        private IConfiguration _config;
        private IProductImageService _productImageService;
        private IProductQuantityService _productQuantityService;
        private IWholePriceService _wholePriceService;
        public ProductDetailController(IProductService productService, IConfiguration config,IProductImageService productImageService,
            IProductQuantityService productQuantityService,IWholePriceService wholePriceService)
        {
            _productService = productService;
            _config = config;
            _productImageService = productImageService;
            _productQuantityService = productQuantityService;
            _wholePriceService = wholePriceService;
            
        }
        [Route("{alias}.p-{id}.html")]
        public IActionResult Index(int id)
        {
            ProductDetailViewModel productDetail = new ProductDetailViewModel() { };
            productDetail.ProductDetail = _productService.GetById(id);
            productDetail.ProductRelate = _productService.GetProductRelate(productDetail.ProductDetail.CategoryId,6);
            productDetail.ProductUpsell = _productService.GetProductUpsell(6);
            productDetail.ProductTags = _productService.GetTagByProductId(id);
            productDetail.DomainApi= _config["DomainApi:Domain"];
            productDetail.ProductImages = _productImageService.GetProductImageByProdutId(id);
            productDetail.Colors = _productQuantityService.GetColorByProductId(id);
            productDetail.Sizes = _productQuantityService.GetSizeByProductId(id);
            productDetail.WholePrices = _wholePriceService.GetAllByProductId(id);
            return View(productDetail);
        }
    }
}