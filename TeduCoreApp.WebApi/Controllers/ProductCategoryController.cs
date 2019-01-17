using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
    public class ProductCategoryController : ApiController
    {
        private IProductCategoryService _productCategoryService;
        private IHostingEnvironment _env;
        private readonly IAuthorizationService _authorizationService;

        public ProductCategoryController(IProductCategoryService productCategoryService, IHostingEnvironment env, IAuthorizationService authorizationService)
        {
            _productCategoryService = productCategoryService;
            _env = env;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("getallhierachy")]
        public async Task<IActionResult> Get()
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT_CATEGORY", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_productCategoryService.GetAll());
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> Get(string filter = "")
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT_CATEGORY", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_productCategoryService.GetAll(filter));
        }

        [HttpGet]
        [Route("detail/{id:int}")]
        public IActionResult Detail(int id)
        {
            return new OkObjectResult(_productCategoryService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] ProductCategoryViewModel productCategoryVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT_CATEGORY", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                _productCategoryService.Add(productCategoryVm);
                _productCategoryService.SaveChanges();
                return new OkObjectResult(productCategoryVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ProductCategoryViewModel productCategoryVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT_CATEGORY", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                ProductCategory productCategoryDb = _productCategoryService.GetByIdDb(productCategoryVm.Id);
                string oldPath = productCategoryDb.Image;
                if (oldPath != productCategoryVm.Image && !string.IsNullOrEmpty(oldPath))
                {
                    oldPath.DeletementByString(_env);
                }
                productCategoryDb.UpdateProductCategory(productCategoryVm);
                _productCategoryService.UpdateDb(productCategoryDb);
                _productCategoryService.SaveChanges();
                return new OkObjectResult(productCategoryVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT_CATEGORY", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            try
            {
                string pathImage = _productCategoryService.GetById(id).Image;
                _productCategoryService.Delete(id);
                _productCategoryService.SaveChanges();
                if (!string.IsNullOrEmpty(pathImage))
                {
                    pathImage.DeletementByString(_env);
                }
                return new OkObjectResult(id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

       
    }
}