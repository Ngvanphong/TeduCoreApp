using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Models;
using static TeduCoreApp.Utilities.Constants.CommonConstants;

namespace TeduCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private ISlideService _slideService;
        private IAdvertistmentService _advertistmentService;
        private IBlogService _blogService;
        private IConfiguration _config;

        public HomeController(IProductService productService, ISlideService slideService,
            IAdvertistmentService advertistmentService, IBlogService blogService, IConfiguration config)
        {
            _productService = productService;
            _slideService = slideService;
            _advertistmentService = advertistmentService;
            _blogService = blogService;
            _config = config;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVm = new HomeViewModel() { };
            homeVm.ListHotProduct = _productService.GetHotProduct(8);
            homeVm.ListNewProduct = _productService.GetNewProduct(8);
            homeVm.ListPromotionProduct = _productService.GetPromotionProduct(8);
            homeVm.ListSlide = _slideService.GetAll(false);
            homeVm.ListAdvertistmentBottom = _advertistmentService.GetbyPageAndPosition(PageName.Home, PositonName.Bottom);
            homeVm.ListAdvertistmentTop = _advertistmentService.GetbyPageAndPosition(PageName.Home, PositonName.Top);
            homeVm.ListBlog = _blogService.GetAll();
            homeVm.DomainApi = _config["DomainApi:Domain"];
            return View(homeVm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}