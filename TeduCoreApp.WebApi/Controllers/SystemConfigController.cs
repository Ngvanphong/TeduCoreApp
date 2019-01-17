using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.SystemConfig;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
    
    public class SystemConfigController : ApiController
    {
        private ISystemConfigService _systemConfigService;
        private readonly IAuthorizationService _authorizationService;

        public SystemConfigController(ISystemConfigService systemConfigService, IAuthorizationService authorizationService)
        {
            _systemConfigService = systemConfigService;
            _authorizationService = authorizationService;
        }
        [Route("getall")]
        [HttpGet]
        public IActionResult Get()
        {           
            return new OkObjectResult(_systemConfigService.GetAll());
        }

        [Route("detail/{id}")]
        [HttpGet]
        public IActionResult Detail(string id)
        {
            return new OkObjectResult(_systemConfigService.Detail(id));
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SystemConfigViewModel systemConfigVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "SYSTEMCONFIG", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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
        public async Task<IActionResult> Update([FromBody] SystemConfigViewModel systemConfigVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "SYSTEMCONFIG", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "SYSTEMCONFIG", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            _systemConfigService.Delete(id);
            _systemConfigService.SaveChanges();
            return new OkObjectResult(id);
        }

    }
}