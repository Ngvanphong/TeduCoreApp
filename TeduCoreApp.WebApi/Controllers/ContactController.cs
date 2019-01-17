using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Contact;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.WebApi.Authorization;

namespace TeduCoreApp.WebApi.Controllers
{
  
    public class ContactController : ApiController
    {
        private IContactService _contactService;
        private readonly IAuthorizationService _authorizationService;
        public ContactController(IContactService contactService, IAuthorizationService authorizationService)
        {
            _contactService = contactService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("getall")]       
        public async Task<IActionResult> Get()
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "CONTACT", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_contactService.GetContact());
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]ContactViewModel contactVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "CONTACT", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _contactService.Add(contactVm);
                    _contactService.SaveChanges();
                    return new OkObjectResult(contactVm.Id);
                }
                catch(Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }              
                
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody]ContactViewModel contactVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "CONTACT", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _contactService.Update(contactVm);
                    _contactService.SaveChanges();
                    return new OkObjectResult(contactVm.Id);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }

            }
            return new BadRequestObjectResult(ModelState);
        }

    }
}