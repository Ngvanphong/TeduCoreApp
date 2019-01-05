using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Extensions;
using TeduCoreApp.Models;
using TeduCoreApp.Utilities.Constants;

namespace TeduCoreApp.Controllers
{
    public class WishlistController : Controller
    {
        private IProductService _productService;
        private IProductQuantityService _productQuantityService;
        private IConfiguration _config;
        public WishlistController(IProductService productService, IProductQuantityService productQuantityService,
            IConfiguration config)
        {
            _productService = productService;
            _productQuantityService = productQuantityService;
            _config = config;
        }

        [Route("wishlist.html")]
        public IActionResult Index()
        {
            var wishlistCart = HttpContext.Session.GetList<WishlistViewModel>(CommonConstants.WishlistCart);
            if (wishlistCart == null)
            {
                wishlistCart = new List<WishlistViewModel>();
            }
            string domainApi = _config["DomainApi:Domain"];
            ViewBag.DomainApi = domainApi;
            return View(wishlistCart);
        }

        [Route("wishlist/addItem")]
        public IActionResult AddToCart(int productId)
        {
            var wishlistCart = HttpContext.Session.GetList<WishlistViewModel>(CommonConstants.WishlistCart);
            if (wishlistCart == null)
            {
                wishlistCart = new List<WishlistViewModel>();
            }          
            if (wishlistCart.Any(x => x.ProductId == productId)==false)
            {
                ProductViewModel product = _productService.GetById(productId);
                WishlistViewModel cart = new WishlistViewModel()
                {
                    ProductId = productId,                    
                    ProductVm = product,
                };
                wishlistCart.Add(cart);
            }
           
            HttpContext.Session.SetList<WishlistViewModel>(CommonConstants.WishlistCart, wishlistCart);          
            return new OkObjectResult(productId);
        }


        [HttpPost]
        [Route("wishlist/removeItem")]
        public IActionResult RemoveCartItem(int productId)
        {
            var wishlistCart = HttpContext.Session.GetList<WishlistViewModel>(CommonConstants.WishlistCart);

            foreach (var item in wishlistCart)
            {
                if (item.ProductId == productId)
                {
                    wishlistCart.Remove(item);
                    break;
                }
            }
            HttpContext.Session.SetList<WishlistViewModel>(CommonConstants.WishlistCart, wishlistCart);
            return new OkObjectResult(productId);
        }

    }
}