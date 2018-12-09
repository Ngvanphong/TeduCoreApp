using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Product;

namespace TeduCoreApp.WebApi.Controllers
{

    public class WholePriceController : ApiController
    {
        private IWholePriceService _wholePriceService;
        public WholePriceController(IWholePriceService wholePriceService)
        {
            _wholePriceService = wholePriceService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult Get(int productId)
        {
            return new OkObjectResult(_wholePriceService.GetAllByProductId(productId));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] WholePriceViewModel wholePriceVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _wholePriceService.Add(wholePriceVm);
                    _wholePriceService.SaveChanges();
                    return new OkObjectResult(wholePriceVm);
                }
                catch(Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] WholePriceViewModel wholePriceVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _wholePriceService.Update(wholePriceVm);
                    _wholePriceService.SaveChanges();
                    return new OkObjectResult(wholePriceVm);
                }
                catch(Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            _wholePriceService.Delete(id);
            _wholePriceService.SaveChanges();
            return new OkObjectResult(id);
        }

    }
}