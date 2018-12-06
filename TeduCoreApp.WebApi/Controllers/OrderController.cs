using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Signalr;

namespace TeduCoreApp.WebApi.Controllers
{

    public class OrderController : ApiController
    {
        private IBillService _billService;
        private WebHub _webHup;

        public OrderController(IBillService billService,WebHub webHup)
        {
            _billService = billService;
            _webHup = webHup;
        }
        [HttpGet]
        [Route("getlistpaging")]
        public IActionResult Get(string startDate, string endDate,
            string customerName, BillStatus billStatus, int pageSize, int page = 1)
        {
            int totalRows = 0;
            List<BillViewModel> listBillVm = _billService.GetList(startDate, endDate, customerName, billStatus, page, pageSize, out totalRows);
            return new OkObjectResult(new ApiResultPaging<BillViewModel>() {
                Items = listBillVm,
                TotalRows = totalRows,
                PageIndex=page,
                PageSize=pageSize,
            });
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] BillViewModel billVm)
        {
            if (ModelState.IsValid)
            {
                _billService.Add(billVm);
                foreach(var billDetail in billVm.BillDetails)
                {
                    _billService.AddBillDetail(billDetail);
                }
                _billService.SaveChanges();
                await _webHup.NewMessage(billVm);               
            }
            return new BadRequestObjectResult(ModelState);
        }

    }
}