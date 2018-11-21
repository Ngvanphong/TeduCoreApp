using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.WebApi.Controllers
{
    public class ProductCategoryController : ApiController
    {
        private IProductCategoryService _productCategoryService;
        private IUnitOfWork _unitOfWork;
        private IHostingEnvironment _env;

        public ProductCategoryController(IProductCategoryService productCategoryService, IUnitOfWork unitOfWork, IHostingEnvironment env)
        {
            _productCategoryService = productCategoryService;
            _unitOfWork = unitOfWork;
            _env = env;
        }

        [HttpGet]
        [Route("getallhierachy")]
        public IActionResult Get()
        {
            return new OkObjectResult(_productCategoryService.GetAll());
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult Get(string filter = "")
        {
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
        public IActionResult Add([FromBody] ProductCategoryViewModel productCategoryVm)
        {
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
        public IActionResult Update([FromBody] ProductCategoryViewModel productCategoryVm)
        {
            if (ModelState.IsValid)
            {
                string oldPath = _productCategoryService.GetById(productCategoryVm.Id).Image;
                if (oldPath != productCategoryVm.Image && !string.IsNullOrEmpty(oldPath))
                {
                    DeleteElementImage(oldPath);
                }
                _productCategoryService.Update(productCategoryVm);
                _productCategoryService.SaveChanges();
                return new OkObjectResult(productCategoryVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                string pathImage = _productCategoryService.GetById(id).Image;
                _productCategoryService.Delete(id);
                _productCategoryService.SaveChanges();
                if (!string.IsNullOrEmpty(pathImage))
                {
                    DeleteElementImage(pathImage);
                }
                return new OkObjectResult(id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public void DeleteElementImage(string path)
        {
            string webHost = _env.WebRootPath;
            string fullPath = webHost + path;
            System.IO.File.Delete(fullPath);
        }
    }
}