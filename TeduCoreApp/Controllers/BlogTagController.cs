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
    public class BlogTagController : Controller
    {
        private IBlogService _blogService;
        private IAdvertistmentService _advertistmentService;
        private IConfiguration _config;
        public BlogTagController(IBlogService blogService, IAdvertistmentService advertistmentService,
            IConfiguration config)
        {
            _blogService = blogService;
            _advertistmentService = advertistmentService;
            _config = config;
        }

        [Route("bt-{id}.html")]
        public IActionResult Index(string id, int page = 1, int pageSize = 3)
        {
            BlogTagIndexViewModel blogTagIndex = new BlogTagIndexViewModel() { };
            blogTagIndex.ResultPagging.Items = _blogService.GetBlogByTagPagging(id,page, pageSize, out int totalRows);
            blogTagIndex.ResultPagging.PageIndex = page;
            blogTagIndex.ResultPagging.PageSize = pageSize;
            blogTagIndex.ResultPagging.TotalRows = totalRows;
            blogTagIndex.DomainApi = _config["DomainApi:Domain"];
            blogTagIndex.Tags = _blogService.GetTagBlogTop(15);
            blogTagIndex.Advertistments = _advertistmentService.GetbyPageAndPosition(PageName.Orther, PositionName.Default);
            return View(blogTagIndex);
        }
    }
}