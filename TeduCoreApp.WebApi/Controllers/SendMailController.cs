using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Subcrible;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.ViewModel;

namespace TeduCoreApp.WebApi.Controllers
{

    public class SendMailController : ApiController
    {
        private ISendMailService _senMailService;
        private ISubcribleService _subcribleService;
        private readonly IAuthorizationService _authorizationService;
        public SendMailController(ISendMailService senMailService, ISubcribleService subcribleService, IAuthorizationService authorizationService)
        {
            _senMailService = senMailService;
            _subcribleService = subcribleService;
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("subcrible")]
        public async Task<IActionResult> SendMuliMail([FromBody] SendEmailViewModel sendEmailVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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
        public async Task<IActionResult> GetSubcrible(int page,int pageSize)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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
        public async Task<IActionResult> Delete(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "USER", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            _subcribleService.Delete(id);
            _subcribleService.SaveChanges();
            return new OkObjectResult(id);
        }

    }
}