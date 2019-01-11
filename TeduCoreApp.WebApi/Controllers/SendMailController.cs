using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Subcrible;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.ViewModel;

namespace TeduCoreApp.WebApi.Controllers
{

    public class SendMailController : ApiController
    {
        private ISendMailService _senMailService;
        private ISubcribleService _subcribleService;
        public SendMailController(ISendMailService senMailService, ISubcribleService subcribleService)
        {
            _senMailService = senMailService;
            _subcribleService = subcribleService;
        }

        [HttpPost]
        [Route("subcrible")]
        public async Task<IActionResult> SendMuliMail([FromBody] SendEmailViewModel sendEmailVm)
        {
            if (ModelState.IsValid)
            {              
                try
                {
                    sendEmailVm.ListEmail = _subcribleService.GetAllEmail();
                    var accesToken = Request.Headers["Authorization"];
                    await _senMailService.SendMultiEmail(sendEmailVm.ListEmail, sendEmailVm.Subject, sendEmailVm.Message, accesToken);
                    return new OkObjectResult(sendEmailVm);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(sendEmailVm);
                }
            }
            return new BadRequestObjectResult(ModelState);                      
            
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetSubcrible(int page,int pageSize)
        {
            List<SubcribleViewModel> listSubs = _subcribleService.GetPaging(page, pageSize, out int totalRows);
            return new OkObjectResult(new ApiResultPaging<SubcribleViewModel>()
            {
                Items=listSubs,
                PageIndex=page,
                PageSize=pageSize,
                TotalRows=totalRows
            });
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            _subcribleService.Delete(id);
            _subcribleService.SaveChanges();
            return new OkObjectResult(id);
        }

    }
}