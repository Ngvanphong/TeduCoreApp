using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.WebApi.Controllers
{

    public class ProductQuantityController : ApiController
    {
        IProductQuantityService _productQuantityService;

        public ProductQuantityController(IProductQuantityService productQuantityService)
        {
            _productQuantityService = productQuantityService;
        }

        [HttpGet]
        [Route("getsizes")]
        public IActionResult GetSize()
        {
            return new OkObjectResult(_productQuantityService.GetListSize());
        }

        [HttpGet]
        [Route("getcolors")]
        public IActionResult GetColor()
        {
            return new OkObjectResult(_productQuantityService.GetListColor());
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult Get(int productId)
        {
            return new OkObjectResult(_productQuantityService.GetAll(productId));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] ProductQuantityViewModel productQuantityVm)
        {
            if (ModelState.IsValid)
            {
                if (_productQuantityService.CheckExist(productQuantityVm.ProductId, productQuantityVm.SizeId, productQuantityVm.ColorId))
                {
                    return new BadRequestObjectResult(ModelState);
                }
                _productQuantityService.Add(productQuantityVm);
                _productQuantityService.SaveChanges();
                return new OkObjectResult(productQuantityVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] ProductQuantityViewModel productQuantityVm)
        {
            if (ModelState.IsValid)
            {
                _productQuantityService.Update(productQuantityVm);
                _productQuantityService.SaveChanges();
                return new OkObjectResult(productQuantityVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int productId, int sizeId, int colorId)
        {
            _productQuantityService.Delete(productId,sizeId,colorId);
            _productQuantityService.SaveChanges();
            return new OkObjectResult(productId);
        }

    }
}