using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
 
    public class ProductImageController : ApiController
    {
        private IProductImageService _productImageService;
        private IHostingEnvironment _env;
        public ProductImageController(IProductImageService productImageService,IHostingEnvironment env)
        {
            _productImageService = productImageService;
            _env = env;
        }
        
        [HttpGet]
        [Route("getallImageContent")]
        public IActionResult GetIamgeContent(int productId)
        {
            return new OkObjectResult(_productImageService.GetProductImageContentByProdutId(productId));
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult Get(int productId)
        {
            return new OkObjectResult(_productImageService.GetProductImageByProdutId(productId));
        }


        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] ProductImageViewModel productImageVm)
        {
            if (ModelState.IsValid)
            {
                _productImageService.Add(productImageVm);
                _productImageService.SaveChanges();
                return new OkObjectResult(productImageVm);
            }
            return new BadRequestObjectResult(ModelState);
        }
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] ProductImageViewModel productImageVm)
        {
            if (ModelState.IsValid)
            {
                _productImageService.Update(productImageVm);
                _productImageService.SaveChanges();
                return new OkObjectResult(productImageVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete] 
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            ProductImageViewModel productImageVm = _productImageService.GetById(id);
            _productImageService.Delete(id);
            _productImageService.SaveChanges();
            productImageVm.Path.DeletementByString(_env);
            return new OkObjectResult(id);
        }
       
    }
}