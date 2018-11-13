using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private IProductService _productService;
        private IProductImageService _productImageService;
        private IHostingEnvironment _env;

        public ProductController(IProductService productService, IProductImageService productImageService, IHostingEnvironment env)
        {
            _productService = productService;
            _productImageService = productImageService;
            _env = env;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll(string keyword, int? categoryId, string filterHotPromotion, int pageSize = 10, int page = 1)
        {
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

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            List<ProductImageViewModel> listProductImageVm = _productImageService.GetProductImageByProdutId(id);
            _productService.Delete(id);
            _productService.SaveChanges();
            for (int i = 0; i < listProductImageVm.Count(); i++)
            {
                DeleteElementImage(listProductImageVm[i].Path);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("deletemulti")]
        public IActionResult Delete(int[] checkedProducts)
        {
            return Ok();
        }

        private void DeleteElementImage(string path)
        {
            var webHost = _env.WebRootPath;
            var fullPath = System.IO.Path.Combine(webHost, path);
            System.IO.File.Delete(fullPath);
        }
    }
}