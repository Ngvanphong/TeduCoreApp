using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Slide;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{

    public class SlideController : ApiController
    {
        private ISlideService _slideService;
        private IHostingEnvironment _env;

        public SlideController(ISlideService slideService, IHostingEnvironment env)
        {
            _slideService = slideService;
            _env = env;
        }

        [HttpGet]
        [Route("getallPagging")]
        public IActionResult Get(int page,int pageSize, string filter)
        {
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
        public IActionResult Add([FromBody] SlideViewModel slideVm)
        {
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
        public IActionResult Update([FromBody] SlideViewModel slideVm)
        {
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
        public IActionResult Delete(int id)
        {
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