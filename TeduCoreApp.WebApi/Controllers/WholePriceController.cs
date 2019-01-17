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

    public class WholePriceController : ApiController
    {
        private IWholePriceService _wholePriceService;
        private readonly IAuthorizationService _authorizationService;
        public WholePriceController(IWholePriceService wholePriceService, IAuthorizationService authorizationService)
        {
            _wholePriceService = wholePriceService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult Get(int productId)
        {
            return new OkObjectResult(_wholePriceService.GetAllByProductId(productId));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] WholePriceViewModel wholePriceVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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
        public async Task<IActionResult> Update([FromBody] WholePriceViewModel wholePriceVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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
        public async Task<IActionResult> Delete(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            _wholePriceService.Delete(id);
            _wholePriceService.SaveChanges();
            return new OkObjectResult(id);
        }

    }
}