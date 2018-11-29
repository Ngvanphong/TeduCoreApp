using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Blog;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
    public class PostImageController : ApiController
    {
        private IBlogImageService _blogImageService;
        private IHostingEnvironment _env;

        public PostImageController(IBlogImageService blogImageService, IHostingEnvironment env)
        {
            _env = env;
            _blogImageService = blogImageService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult Get(int blogId)
        {
            return new OkObjectResult(_blogImageService.GetAllByBlogId(blogId));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] BlogImageViewModel blogImageVm)
        {
            if (ModelState.IsValid)
            {
                _blogImageService.Add(blogImageVm);
                _blogImageService.SaveChanges();
                return new OkObjectResult(blogImageVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(int id, string caption)
        {
            BlogImage blogImageDb = _blogImageService.GetByIdDb(id);
            blogImageDb.Caption = caption;
            _blogImageService.Update(blogImageDb);
            _blogImageService.SaveChanges();
            return new OkObjectResult(id);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            BlogImageViewModel blogImageVm = _blogImageService.GetById(id);
            _blogImageService.Delete(id);
            _blogImageService.SaveChanges();
            string pathImage = blogImageVm.Path;
            if (!string.IsNullOrEmpty(pathImage))
            {
                pathImage.DeletementByString(_env);
            }
            return new OkObjectResult(id);
        }
    }
}