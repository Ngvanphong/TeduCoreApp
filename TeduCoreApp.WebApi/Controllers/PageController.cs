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
using TeduCoreApp.Data.ViewModels.Page;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
   
    public class PageController : ApiController
    {
        private IPageService _pageService;
        private IPageImageService _pageImageService;
        private IHostingEnvironment _env;
        private readonly IAuthorizationService _authorizationService;
        public PageController(IPageService pageService, IPageImageService pageImageService, IHostingEnvironment env, IAuthorizationService authorizationService)
        {
            _pageService = pageService;
            _pageImageService = pageImageService;
            _env = env;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> Get()
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PAGE", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_pageService.GetAll());
        }
        [HttpGet]
        [Route("detail/{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PAGE", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_pageService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] PageViewModel pageVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PAGE", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                int id = _pageService.Add(pageVm);              
                return new OkObjectResult(new { Id = id });
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] PageViewModel pageVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PAGE", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                _pageService.Update(pageVm);
                _pageService.SaveChanges();
                return new OkObjectResult(pageVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PAGE", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            try
            {
                List<PageImageViewModel> listPageImage = _pageImageService.GetAllByPageId(id);
                _pageService.Delete(id);
                _pageService.SaveChanges();
                foreach(var item in listPageImage)
                {
                    item.Path.DeletementByString(_env);
                }
                return new OkObjectResult(id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}