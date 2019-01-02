using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Models;
using static TeduCoreApp.Utilities.Constants.CommonConstants;

namespace TeduCoreApp.Controllers
{
    public class BlogDetailController : Controller
    {
        private IBlogService _blogService;
        private IAdvertistmentService _advertistmentService;
        private IConfiguration _config;

        public BlogDetailController(IBlogService blogService, IAdvertistmentService advertistmentService,
            IConfiguration config)
        {
            _blogService = blogService;
            _advertistmentService = advertistmentService;
            _config = config;
        }
        [Route("{alias}.b-{id}.html")]
        public IActionResult Index(int id)
        {
            BlogDetailIndexViewModel blogDetail = new BlogDetailIndexViewModel() { };
            blogDetail.Bog = _blogService.GetById(id);
            blogDetail.TagsForBlogDetail = _blogService.GetTagByBlogId(id);
            blogDetail.DomainApi = _config["DomainApi:Domain"];
            blogDetail.Tags = _blogService.GetTagBlogTop(15);
            blogDetail.Advertistments = _advertistmentService.GetbyPageAndPosition(PageName.Orther, PositionName.Default);
            return View(blogDetail);
        }
    }
}