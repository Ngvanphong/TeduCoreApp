using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Extensions;
using TeduCoreApp.Models;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;
using Microsoft.Extensions.Configuration;

namespace TeduCoreApp.Controllers
{
    public class ShoppingCardController : Controller
    {
        private IProductService _productService;
        private IProductQuantityService _productQuantityService;
        private IConfiguration _config;
        public ShoppingCardController(IProductService productService, IProductQuantityService productQuantityService,
            IConfiguration config)
        {
            _productService = productService;
            _productQuantityService = productQuantityService;
            _config = config;
        }
        [Route("shopping/getall")]
        public IActionResult Index()
        {
            var shoppingCart = HttpContext.Session.GetList<ShoppingCardViewModel>(CommonConstants.SesstionCart);
            if (shoppingCart == null)
            {
                shoppingCart = new List<ShoppingCardViewModel>();
            }
            string domainApi= _config["DomainApi:Domain"];
            int countProduct = shoppingCart.Count();
            return new OkObjectResult(new { Items=shoppingCart,DomainApi=domainApi,CountProduct=countProduct});
        }

        [HttpPost]
        [Route("shopping/addToCart")]
        public IActionResult AddToCart(int productId, int colorId, int sizeId, int quantity)
        {
            var shoppingCart = HttpContext.Session.GetList<ShoppingCardViewModel>(CommonConstants.SesstionCart);
            if (shoppingCart== null)
            {
                shoppingCart = new List<ShoppingCardViewModel>();
            }

            SizeViewModel sizeVm = _productQuantityService.GetSizeById(sizeId);
            ColorViewModel colorVm = _productQuantityService.GetColorById(colorId);

            if (shoppingCart.Any(x => x.ProductId == productId && x.SizeVm.Id == sizeId && x.ColorVm.Id == colorId))
            {
                foreach (var item in shoppingCart)
                {

                    if (item.ProductId == productId && item.SizeVm.Id == sizeId&&item.ColorVm.Id==colorId)
                    {
                        item.Quantity += quantity;
                        break;
                    }
                }
            }
            else
            {
                ProductViewModel product = _productService.GetById(productId);
                ShoppingCardViewModel cart = new ShoppingCardViewModel()
                {
                    ProductId = productId,                  
                    SizeVm = sizeVm,
                    ColorVm = colorVm,
                    Quantity=quantity,
                    ProductVm=product,
                };
                shoppingCart.Add(cart);
            }
            HttpContext.Session.SetList<ShoppingCardViewModel>(CommonConstants.SesstionCart, shoppingCart);
            shoppingCart = HttpContext.Session.GetList<ShoppingCardViewModel>(CommonConstants.SesstionCart);
            return new OkObjectResult(productId);
        }

        [HttpPost]
        [Route("shopping/removeCartItem")]
        public IActionResult RemoveCartItem(int productId,int sizeId,int colorId)
        {
            var shoppingCart = HttpContext.Session.GetList<ShoppingCardViewModel>(CommonConstants.SesstionCart);

            foreach (var item in shoppingCart)
            {

                if (item.ProductId == productId && item.SizeVm.Id == sizeId && item.ColorVm.Id == colorId)
                {
                    shoppingCart.Remove(item);
                    break;
                }
            }
            HttpContext.Session.SetList<ShoppingCardViewModel>(CommonConstants.SesstionCart, shoppingCart);
            return new OkObjectResult(productId);
        }

        [HttpPost]
        [Route("shopping/updateShopping")]
        public IActionResult UpdateShoppingCart(int productId,int colorId, int sizeId,int quantity)
        {
            var shoppingCart = HttpContext.Session.GetList<ShoppingCardViewModel>(CommonConstants.SesstionCart);
            foreach (var item in shoppingCart)
            {

                if (item.ProductId == productId && item.SizeVm.Id == sizeId && item.ColorVm.Id == colorId)
                {
                    item.Quantity = quantity;
                    break;
                }
            }
            HttpContext.Session.SetList<ShoppingCardViewModel>(CommonConstants.SesstionCart, shoppingCart);
            return new OkObjectResult(productId);
        }
    }
}