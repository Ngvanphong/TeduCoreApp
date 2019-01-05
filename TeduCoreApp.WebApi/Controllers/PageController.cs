using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Page;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
   
    public class PageController : ApiController
    {
        private IPageService _pageService;
        private IPageImageService _pageImageService;
        private IHostingEnvironment _env;
        public PageController(IPageService pageService, IPageImageService pageImageService, IHostingEnvironment env)
        {
            _pageService = pageService;
            _pageImageService = pageImageService;
            _env = env;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult Get()
        {
            return new OkObjectResult(_pageService.GetAll());
        }
        [HttpGet]
        [Route("detail/{id:int}")]
        public IActionResult Detail(int id)
        {
            return new OkObjectResult(_pageService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] PageViewModel pageVm)
        {
            if (ModelState.IsValid)
            {
                int id = _pageService.Add(pageVm);              
                return new OkObjectResult(new { Id = id });
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] PageViewModel pageVm)
        {
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
        public IActionResult Delete(int id)
        {
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