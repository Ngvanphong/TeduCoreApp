using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Tag;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.WebApi.Controllers
{
    public class TagController : ApiController
    {
        private ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Route("getall")]
        [HttpGet]
        public IActionResult Get(int page, int pageSize, string filter=null)
        {
            List<TagViewModel> listTag = _tagService.GetAllPagging(page, pageSize, out int totalRows,filter);
            return new OkObjectResult(new ApiResultPaging<TagViewModel>()
            {
                Items=listTag,
                PageIndex=page,
                PageSize=pageSize,
                TotalRows=totalRows
            });
        }
   
        [HttpDelete]
        [Route("deletealltagnotuse")]
        public IActionResult DeleteMulti()
        {
            _tagService.DeleteMultiNotUse();
            _tagService.SaveChanges();
            return new OkObjectResult("success");
        }
    }
}