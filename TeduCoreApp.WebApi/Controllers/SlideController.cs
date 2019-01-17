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
using TeduCoreApp.Data.ViewModels.Slide;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{

    public class SlideController : ApiController
    {
        private ISlideService _slideService;
        private IHostingEnvironment _env;
        private readonly IAuthorizationService _authorizationService;
        public SlideController(ISlideService slideService, IHostingEnvironment env, IAuthorizationService authorizationService)
        {
            _slideService = slideService;
            _env = env;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("getallPagging")]
        public async Task<IActionResult> Get(int page,int pageSize, string filter)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "SLIDE", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            int totalRows = 0;
            List<SlideViewModel> listSlideVm = _slideService.GetAllPagging(page, pageSize, filter, out totalRows);
            return new OkObjectResult(new ApiResultPaging<SlideViewModel>()
            {
                Items = listSlideVm,
                PageIndex = page,
                PageSize = pageSize,
                TotalRows = totalRows
            });
        }

        [HttpGet]
        [Route("detail/{id:int}")]
        public IActionResult Detail(int id)
        {
            return new OkObjectResult(_slideService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] SlideViewModel slideVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "SLIDE", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                _slideService.Add(slideVm);
                _slideService.SaveChanges();
                return new OkObjectResult(slideVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] SlideViewModel slideVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "SLIDE", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                Slide slideDb = _slideService.GetByIdDb(slideVm.Id);
                string oldPath = slideDb.Image;
                if (oldPath != slideVm.Image && !string.IsNullOrEmpty(oldPath))
                {
                    oldPath.DeletementByString(_env);
                }
                slideDb.UpdateSlide(slideVm);
                _slideService.Update(slideDb);
                _slideService.SaveChanges();
                return new OkObjectResult(slideVm);

            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "SLIDE", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            string oldPath = _slideService.GetByIdDb(id).Image;
            if (!string.IsNullOrEmpty(oldPath))
            {
                oldPath.DeletementByString(_env);
            }
            _slideService.Delete(id);
            _slideService.SaveChanges();
            return new OkObjectResult(id);

        }
    }
}