using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Extensions;
using TeduCoreApp.Models;
using TeduCoreApp.Utilities.Constants;

namespace TeduCoreApp.Controllers
{
    public class CheckoutController : Controller
    {
        private IHostingEnvironment _env;
        private UserManager<AppUser> _userManager;
        private IBillService _billService;
        private IConfiguration _config;

        public CheckoutController(IHostingEnvironment env, UserManager<AppUser> userManager, IBillService billService,IConfiguration config)
        {
            _env = env;
            _userManager = userManager;
            _billService = billService;
            _config = config;
        }

        [Route("checkout.html")]
        public IActionResult Index()
        {
            ViewData["DomainApi"]= _config["DomainApi:Domain"];
            return View();
        }

        [Route("checkout/loadDistrict")]
        [HttpPost]
        public IActionResult LoadDistrict(int provinceId)
        {
            string webHost = _env.WebRootPath;
            var xmlDoc = XDocument.Load(webHost+(@"/client-side/xml/Provinces_HCM.xml"));
            var xmlElement = xmlDoc.Element("Root").Elements("Item").Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceId);
            List<DistrictViewModel> listDistrict = new List<DistrictViewModel>();
            foreach (var item in xmlElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                DistrictViewModel district = new DistrictViewModel()
                {
                    Id = int.Parse(item.Attribute("id").Value),
                    Name = item.Attribute("value").Value,
                };
                listDistrict.Add(district);
            }
            return new OkObjectResult(new { status = true, data = listDistrict });
        }

        [Route("checkout/GetTaxHCM")]
        [HttpPost]
        public IActionResult GetTaxHCM(int districtId)
        {
            string taxTransfar="12.000";
            List<int> listOutsiteDistrict = new List<int>()
            {
                70139,70143,70135,70137,70141,70134,70133
            };
            foreach (var item in listOutsiteDistrict)
            {
                if (item == districtId)
                {
                    taxTransfar = "14.000";
                }
            }
            return new OkObjectResult(new { status = true, data = taxTransfar });
        }

        [HttpPost]
        [Route("checkout.html")]
        public async Task<IActionResult> Checkout(BillViewModel billVm,decimal feeShipping,decimal totalMoneyOrder,decimal totalMoneyPayment)
        {
            try
            {
                var shoppingCart = HttpContext.Session.GetList<ShoppingCardViewModel>(CommonConstants.SesstionCart);
                BillViewModel billViewModel;
                billViewModel = billVm;
                billViewModel.FeeShipping = feeShipping;
                billViewModel.TotalMoneyOrder = totalMoneyOrder;
                billViewModel.TotalMoneyPayment = totalMoneyPayment;
                billViewModel.BillStatus = Data.Enums.BillStatus.New;
                billViewModel.Status = Data.Enums.Status.Active;
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    billViewModel.CustomerId = user.Id;
                } 
                var listBillDetails = new List<BillDetailViewModel>();
                foreach (var item in shoppingCart)
                {
                    decimal salePrice;
                    if (item.ProductVm.PromotionPrice.HasValue)
                    {
                        salePrice = (decimal)item.ProductVm.PromotionPrice;
                    }
                    else
                    {
                        salePrice = item.ProductVm.Price;
                    }
                    BillDetailViewModel billDetailVm = new BillDetailViewModel()
                    {
                       
                        ProductId = item.ProductId,
                        ColorId = item.ColorVm.Id,
                        SizeId = item.SizeVm.Id,
                        Quantity = item.Quantity,
                        Price = salePrice
                    };
                    listBillDetails.Add(billDetailVm);
                }
                billViewModel.BillDetails = listBillDetails;
                int billId= _billService.Add(billViewModel);                            
                billViewModel.Id = billId;
                billViewModel.DateCreated = DateTime.Now;
                HttpContext.Session.SetList<ShoppingCardViewModel>(CommonConstants.SesstionCart, null);
                return new OkObjectResult(new { status = true,billVm=billViewModel });
            }
            catch
            {
                return new OkObjectResult(new { status = false });
            }
            
        }

    }
}