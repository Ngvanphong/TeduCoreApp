using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.ViewModels.Contact;

namespace TeduCoreApp.Controllers
{
    public class ContactController : Controller
    {
        private IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [Route("contact.html")]
        public IActionResult Index()
        {
            ContactViewModel contactVm = _contactService.GetContact();
            return View(contactVm);
        }
    }
}