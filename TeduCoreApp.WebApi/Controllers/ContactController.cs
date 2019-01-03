using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Contact;

namespace TeduCoreApp.WebApi.Controllers
{
  
    public class ContactController : ApiController
    {
        private IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Route("getall")]       
        public IActionResult Get()
        {
            return new OkObjectResult(_contactService.GetContact());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody]ContactViewModel contactVm)
        {
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
        public IActionResult Update([FromBody]ContactViewModel contactVm)
        {
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