using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Provider;

namespace TeduCoreApp.WebApi.Controllers
{

    public class ProductController : ApiController
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll(string keyword,int? categoryId, string filterHotPromotion, int pageSize=10, int page = 1)
        {
            int totalRow = 0;
            List<ProductViewModel> listProduct = _productService.GetAll(categoryId, filterHotPromotion, keyword, page, pageSize, out totalRow);
            return new OkObjectResult(new ApiResultPaging<ProductViewModel>()
            {
                Items=listProduct,
                PageIndex=page,
                PageSize=pageSize,
                TotalRows=totalRow,               
            });                    
        }
    }
}