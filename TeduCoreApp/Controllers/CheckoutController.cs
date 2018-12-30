using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Models;

namespace TeduCoreApp.Controllers
{
    public class CheckoutController : Controller
    {
        private IHostingEnvironment _env;

        public CheckoutController(IHostingEnvironment env)
        {
            _env = env;
        }

        [Route("checkout.html")]
        public IActionResult Index()
        {
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
        public IActionResult Checkout(CheckoutViewModel checkoutVm)
        {
            return new OkObjectResult("true");
        }

    }
}