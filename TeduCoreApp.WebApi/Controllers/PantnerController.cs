using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Pantner;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{ 
    public class PantnerController : ApiController
    {
        private IPantnerService _pantnerService;
        private IHostingEnvironment _env;
        private readonly IAuthorizationService _authorizationService;
        public PantnerController(IPantnerService pantnerService, IHostingEnvironment env, IAuthorizationService authorizationService)
        {
            _pantnerService = pantnerService;
            _env = env;
            _authorizationService = authorizationService;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> Get()
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PANTNER", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_pantnerService.GetAll());
        }

        [HttpGet]
        [Route("detail/{id:int}")]
        public IActionResult Detail(int id)
        {
            return new OkObjectResult(_pantnerService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] PantnerViewModel pantnerVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PANTNER", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                _pantnerService.Add(pantnerVm);
                _pantnerService.SaveChanges();
                return new OkObjectResult(pantnerVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] PantnerViewModel pantnerVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PANTNER", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                Pantner pantnerDb = _pantnerService.GetByIdDb(pantnerVm.Id);
                string oldPath = pantnerDb.Image;
                if (oldPath != pantnerVm.Image && !string.IsNullOrEmpty(oldPath))
                {
                    oldPath.DeletementByString(_env);
                }
                pantnerDb.UpdatePantner(pantnerVm);
                _pantnerService.Update(pantnerDb);
                _pantnerService.SaveChanges();
                return new OkObjectResult(pantnerVm);

            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PANTNER", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            string oldPath = _pantnerService.GetByIdDb(id).Image;
            if (!string.IsNullOrEmpty(oldPath))
            {
                oldPath.DeletementByString(_env);
            }
            _pantnerService.Delete(id);
            _pantnerService.SaveChanges();
            return new OkObjectResult(id);

        }
    }
}