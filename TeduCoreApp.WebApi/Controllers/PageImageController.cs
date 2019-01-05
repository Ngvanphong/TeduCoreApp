using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Page;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
    public class PageImageController : ApiController
    {
        private IPageImageService _pageImageService;
        private IHostingEnvironment _env;
        public PageImageController(IPageImageService pageImageService, IHostingEnvironment env)
        {
            _pageImageService = pageImageService;
            _env = env;
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult Get(int pageId)
        {
            return new OkObjectResult(_pageImageService.GetAllByPageId(pageId));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] PageImageViewModel pageImageVm)
        {
            if (ModelState.IsValid)
            {
                _pageImageService.Add(pageImageVm);
                _pageImageService.SaveChanges();
                return new OkObjectResult(pageImageVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            PageImageViewModel pageImageVm = _pageImageService.GetById(id);
            _pageImageService.Delete(id);
            _pageImageService.SaveChanges();
            string pathImage = pageImageVm.Path;
            if (!string.IsNullOrEmpty(pathImage))
            {
                pathImage.DeletementByString(_env);
            }
            return new OkObjectResult(id);
        }
    }
}