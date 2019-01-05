using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Extensions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeduCoreApp.WebApi.Controllers
{
    public class PostController : ApiController
    {
        private IBlogService _blogService;
        private IHostingEnvironment _env;
        private IBlogImageService _blogImageService;
           
        public PostController(IBlogService blogService, IHostingEnvironment env, IBlogImageService blogImageService)
        {
            _env = env;
            _blogService = blogService;
            _blogImageService = blogImageService;
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult Get(int page, int pageSize, string keyword)
        {
            int totalRows = 0;
            List<BlogViewModel> listBlog = _blogService.GetAllPaging(keyword, page, pageSize, out totalRows);
            return new OkObjectResult(new ApiResultPaging<BlogViewModel>()
            {
                Items = listBlog,
                TotalRows=totalRows,
                PageIndex=page,
                PageSize=pageSize,
            });
        }
        [HttpGet]
        [Route("detail/{id:int}")]
        public IActionResult Detail(int id)
        {
            return new OkObjectResult(_blogService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] BlogViewModel blogVm)
        {
            if (ModelState.IsValid)
            {
               int id= _blogService.Add(blogVm);
                _blogService.SaveChanges();
                return new OkObjectResult(new { Id = id });
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] BlogViewModel blogVm)
        {
            if (ModelState.IsValid)
            {
                Blog blogDb = _blogService.GetByIdDb(blogVm.Id);
                string oldPath = blogDb.Image;
                if (oldPath != blogVm.Image && !string.IsNullOrEmpty(oldPath))
                {
                    oldPath.DeletementByString(_env);
                }
                blogDb.UpdateBlog(blogVm);
                _blogService.Update(blogDb);
                _blogService.SaveChanges();
                return new OkObjectResult(blogVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                string pathImage = _blogService.GetById(id).Image;
                List<BlogImageViewModel> listBlogImage = _blogImageService.GetAllByBlogId(id);
                _blogService.Delete(id);
                _blogService.SaveChanges();
                if (!string.IsNullOrEmpty(pathImage))
                {
                    pathImage.DeletementByString(_env);
                }
                foreach(var item in listBlogImage)
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
