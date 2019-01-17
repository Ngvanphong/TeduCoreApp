using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.WebApi.Authorization;

namespace TeduCoreApp.WebApi.Controllers
{

    public class ProductQuantityController : ApiController
    {
        IProductQuantityService _productQuantityService;
        private readonly IAuthorizationService _authorizationService;
        public ProductQuantityController(IProductQuantityService productQuantityService, IAuthorizationService authorizationService)
        {
            _productQuantityService = productQuantityService;
            _authorizationService = authorizationService;
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
        public async Task<IActionResult> Add([FromBody] ProductQuantityViewModel productQuantityVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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
        public async Task<IActionResult> Update([FromBody] ProductQuantityViewModel productQuantityVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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
        public async Task<IActionResult> Delete(int productId, int sizeId, int colorId)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            _productQuantityService.Delete(productId,sizeId,colorId);
            _productQuantityService.SaveChanges();
            return new OkObjectResult(productId);
        }

    }
}