using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
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

        [HttpGet]
        [Route("detail/{id:int}")]
        public IActionResult Detail(int id)
        {          
            return new OkObjectResult(_productService.GetById(id));
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] ProductViewModel productVm)
        {         
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
        public IActionResult Update([FromBody] ProductViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(productVm);
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
        public IActionResult Delete(string checkedProducts)
        {     
            List<int>listProductId= Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(checkedProducts);
            foreach (int item in listProductId)
            {
                List<ProductImageViewModel> listProductImageVm = _productImageService.GetProductImageByProdutId(item);
                _productService.Delete(item);
                _productService.SaveChanges();
                for (int i = 0; i < listProductImageVm.Count(); i++)
                {
                    DeleteElementImage(listProductImageVm[i].Path);
                }
            }
            return Ok();
        }

        public void DeleteElementImage(string path)
        {
            string webHost = _env.WebRootPath;
            string fullPath = webHost+ path;
            System.IO.File.Delete(fullPath);
        }
    }
}