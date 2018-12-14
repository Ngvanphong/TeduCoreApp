using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.WebApi.Controllers
{
    
    public class AdvertistmentController : ApiController
    {
        private IAdvertistmentService _advertistmentService;
        private  IHostingEnvironment _env;
        public AdvertistmentController(IAdvertistmentService advertistmentService, IHostingEnvironment env)
        {
            _advertistmentService = advertistmentService;
            _env = env;
        }

        [HttpGet]
        [Route("getallpaging")]
        public IActionResult Get(int page,int pageSize, string filter)
        {
            List<AdvertistmentViewModel> listAdvertistmentVm = _advertistmentService.GetAll(page, pageSize, filter, out int totalRows);

            return new OkObjectResult(new ApiResultPaging<AdvertistmentViewModel>()
            {
                Items = listAdvertistmentVm,
                PageIndex=page,
                PageSize=pageSize,
                TotalRows=totalRows,
            });
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] AdvertistmentViewModel advertistmentVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _advertistmentService.Add(advertistmentVm);
                    _advertistmentService.SaveChanges();
                    return new OkObjectResult(advertistmentVm);
                }
                catch(Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }

            }
            return new BadRequestObjectResult(ModelState);
        }
    }
}