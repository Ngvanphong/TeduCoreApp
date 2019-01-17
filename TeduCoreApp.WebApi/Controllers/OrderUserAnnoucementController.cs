﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Data.ViewModels.BillUserAnnoucement;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.WebApi.Authorization;
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
            BillUserAnnoucement query = _billUserAnnoucementService.GetByUserBill(id, Guid.Parse(userId));
            if (query == null)
            {
                BillUserAnnoucement billUserAnnoucement = new BillUserAnnoucement() { };
                billUserAnnoucement.UserId = Guid.Parse(userId);
                billUserAnnoucement.BillId = id;
                billUserAnnoucement.HasRead = true;
                _billUserAnnoucementService.AddDb(billUserAnnoucement);
                _billUserAnnoucementService.SaveChanges();
                return new OkObjectResult(id);
            }
            else
            {
                query.HasRead = true;
                _billUserAnnoucementService.UpdateDb(query);
                _billUserAnnoucementService.SaveChanges();
                return new OkObjectResult(id);
            }
        }

    }
}