using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
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
        private ISubcribleService _subcribleService;
        private ISystemConfigService _systemConfig;
        public HomeController(IProductService productService, ISlideService slideService,IAdvertistmentService advertistmentService,
            IBlogService blogService, IConfiguration config, ISubcribleService subcribleService, ISystemConfigService systemConfig)
        {
            _productService = productService;
            _slideService = slideService;
            _advertistmentService = advertistmentService;
            _blogService = blogService;
            _config = config;
            _subcribleService = subcribleService;
            _systemConfig = systemConfig;
        }

        public IActionResult Index()
        {
            
            HomeViewModel homeVm = new HomeViewModel() { };
            homeVm.ListHotProduct = _productService.GetHotProduct(8);
            homeVm.ListNewProduct = _productService.GetNewProduct(8);
            homeVm.ListPromotionProduct = _productService.GetPromotionProduct(8);
            homeVm.ListSlide = _slideService.GetAll(false);
            homeVm.ListAdvertistmentBottom = _advertistmentService.GetbyPageAndPosition(PageName.Home, PositionName.Bottom);
            homeVm.ListAdvertistmentTop = _advertistmentService.GetbyPageAndPosition(PageName.Home, PositionName.Top);
            homeVm.ListBlog = _blogService.GetAllForHome(3);
            homeVm.DomainApi = _config["DomainApi:Domain"];
            ViewBag.HomeTitle = _systemConfig.Detail("HomeTitle").Value1;
            ViewBag.HomeMetaDescription = _systemConfig.Detail("HomeMetaDescription").Value1;
            ViewBag.HomeMetaKeyword = _systemConfig.Detail("HomeMetaKeyword").Value1;
            return View(homeVm);
        }

        [Route("subcrible/add")]
        [HttpPost]
        public IActionResult AddSubcrible(string email)
        {
            if (email != null && email != "")
            {
                const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                bool isEmail= regex.IsMatch(email);
                if (isEmail)
                {
                    if (_subcribleService.CheckExit(email) == false)
                    {
                        _subcribleService.Add(email);
                        _subcribleService.SaveChanges();
                        return new OkObjectResult(new { status = true });
                    }
                    else
                    {
                        return new OkObjectResult(new { status = false });
                    }
                }
                return new OkObjectResult(new { status = false });

            }
            return new OkObjectResult(new { status = false });
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}