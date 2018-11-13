using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.WebApi.Provider;

namespace TeduCoreApp.WebApi.Controllers
{
   
    public class ProductCategoryController : ApiController
    {
        private IProductCategoryService _productCategoryService;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryController(IProductCategoryService productCategoryService, IUnitOfWork unitOfWork)
        {
            _productCategoryService = productCategoryService;
            _unitOfWork = unitOfWork;
        }
        [Route("getallhierachy")]
        public IActionResult Get()
        {
            List<ProductCategoryViewModel> listProductCategoryVm = _productCategoryService.GetAll();
            return new OkObjectResult(listProductCategoryVm);
        }
    }
}