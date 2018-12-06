using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Data.ViewModels.BillUserAnnoucement;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
   
    public class OrderUserAnnoucementController : ApiController
    {
        private IBillUserAnnoucementService _billUserAnnoucementService;
        public OrderUserAnnoucementController(IBillUserAnnoucementService billUserAnnoucementService)
        {
            _billUserAnnoucementService = billUserAnnoucementService;
        }

        [HttpGet]
        [Route("getTopMyAnnouncement")]
        public IActionResult GetTopMyAnnouncement()
        {
            string userId = User.GetSpecialClaimsApi("Id");
            List<BillViewModel> listBillVm =_billUserAnnoucementService.ListAllUnread(userId);
            return new OkObjectResult(listBillVm);
        }

        [Route("markAsRead")]
        [HttpGet]
        public IActionResult MarkAsRead( int id)
        {
            string userId = User.GetSpecialClaimsApi("Id");
            BillUserAnnoucementViewModel query = _billUserAnnoucementService.GetById(id);
            if (query == null)
            {
                BillUserAnnoucement billUserAnnoucement = new BillUserAnnoucement();
                billUserAnnoucement.UserId = Guid.Parse(userId);
                billUserAnnoucement.BillId = query.BillId;
                billUserAnnoucement.HasRead = true;
                _billUserAnnoucementService.AddDb(billUserAnnoucement);
                _billUserAnnoucementService.SaveChanges();
                return new OkObjectResult(id);
            }
            else
            {
                query.HasRead = true;
                _billUserAnnoucementService.SaveChanges();
                return new OkObjectResult(id);
            }
        }

    }
}