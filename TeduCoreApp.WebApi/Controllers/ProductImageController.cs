using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
 
    public class ProductImageController : ApiController
    {
        private IProductImageService _productImageService;
        private IHostingEnvironment _env;
        private readonly IAuthorizationService _authorizationService;
        public ProductImageController(IProductImageService productImageService,IHostingEnvironment env, IAuthorizationService authorizationService)
        {
            _productImageService = productImageService;
            _env = env;
            _authorizationService = authorizationService;
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
        public async Task<IActionResult> Add([FromBody] ProductImageViewModel productImageVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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
        public async Task<IActionResult> Update([FromBody] ProductImageViewModel productImageVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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
        public async Task<IActionResult> Delete(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            ProductImageViewModel productImageVm = _productImageService.GetById(id);
            _productImageService.Delete(id);
            _productImageService.SaveChanges();
            productImageVm.Path.DeletementByString(_env);
            return new OkObjectResult(id);
        }
       
    }
}