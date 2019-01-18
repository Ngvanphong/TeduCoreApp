using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private IProductService _productService;
        private IProductImageService _productImageService;
        private IHostingEnvironment _env;
        private readonly IAuthorizationService _authorizationService;

        public ProductController(IProductService productService, IProductImageService productImageService, IHostingEnvironment env,
            IAuthorizationService authorizationService)
        {
            _productService = productService;
            _productImageService = productImageService;
            _env = env;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("getallparents")]
        public async Task<IActionResult> GetAll()
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_productService.GetAll());
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll(string keyword, int? categoryId, string filterHotPromotion, int pageSize = 10, int page = 1)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            int totalRow = 0;
            List<ProductViewModel> listProduct = _productService.GetAll(categoryId, filterHotPromotion, keyword, page, pageSize, out totalRow);
            return new OkObjectResult(new ApiResultPaging<ProductViewModel>()
            {
                Items = listProduct,
                PageIndex = page,
                PageSize = pageSize,
                TotalRows = totalRow,
            });
        }

        [HttpGet]
        [Route("detail/{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_productService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] ProductViewModel productVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(productVm);
                _productService.SaveChanges();
                return new OkObjectResult(productVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ProductViewModel productVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                Product productDb = _productService.GetProductDbById(productVm.Id);
                productDb.UpdateProductDb(productVm);
                _productService.UpdateDb(productDb);
                _productService.SaveChanges();
                return new OkObjectResult(productVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("thumnailImage")]
        public IActionResult UpdateThumbnailImage(int productId)
        {
            Product productDb = _productService.GetProductDbById(productId);
            try
            {
                ProductImageViewModel productImageVm = _productImageService.GetProductImageByProdutId(productId).FirstOrDefault();
                productDb.ThumbnailImage = productImageVm.Path;
                _productService.UpdateDb(productDb);
                _productService.SaveChanges();
                return new OkObjectResult(productId);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Not found image");
            }
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
            List<ProductImageViewModel> listProductImageVm = _productImageService.GetProductImageByProdutId(id);
            _productService.Delete(id);
            _productService.SaveChanges();
            for (int i = 0; i < listProductImageVm.Count(); i++)
            {
                listProductImageVm[i].Path.DeletementByString(_env);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("deletemulti")]
        public async Task<IActionResult> Delete(string checkedProducts)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            List<int> listProductId = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(checkedProducts);
            foreach (int item in listProductId)
            {
                List<ProductImageViewModel> listProductImageVm = _productImageService.GetProductImageByProdutId(item);
                _productService.Delete(item);
                _productService.SaveChanges();
                for (int i = 0; i < listProductImageVm.Count(); i++)
                {
                    listProductImageVm[i].Path.DeletementByString(_env);
                }
            }
            return Ok();
        }
    }
}