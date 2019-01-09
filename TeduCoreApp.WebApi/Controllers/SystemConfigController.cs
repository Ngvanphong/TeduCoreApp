using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.SystemConfig;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
    
    public class SystemConfigController : ApiController
    {
        private ISystemConfigService _systemConfigService;

        public SystemConfigController(ISystemConfigService systemConfigService)
        {
            _systemConfigService = systemConfigService;
        }

        [Route("detail/{id}")]
        [HttpGet]
        public IActionResult Get(string id)
        {
            return new OkObjectResult(_systemConfigService.Detail(id));
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add([FromBody] SystemConfigViewModel systemConfigVm)
        {
            if (ModelState.IsValid)
            {
                _systemConfigService.Add(systemConfigVm);
                _systemConfigService.SaveChanges();
                return new OkObjectResult(systemConfigVm.Id);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [Route("update")]
        [HttpPut]
        public IActionResult Update([FromBody] SystemConfigViewModel systemConfigVm)
        {
            if (ModelState.IsValid)
            {
                SystemConfig systemConfigDb = _systemConfigService.DetailDb(systemConfigVm.Id);
                systemConfigDb.UpdateSystemConfig(systemConfigVm);
                _systemConfigService.Update(systemConfigDb);
                _systemConfigService.SaveChanges();
                return new OkObjectResult(systemConfigVm.Id);
            }
            return new BadRequestObjectResult(ModelState);
        }

    }
}