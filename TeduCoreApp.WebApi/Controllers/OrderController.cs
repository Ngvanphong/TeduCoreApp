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

        public OrderController(IBillService billService)
        {
            _billService = billService;
        }
        [HttpGet]
        [Route("getlistpaging")]
        public IActionResult Get(string startDate, string endDate,
            string customerName, BillStatus billStatus, int pageSize, int page = 1)
        {           
            List<BillViewModel> listBillVm = _billService.GetList(startDate, endDate, customerName, billStatus, page, pageSize, out int totalRows);
            return new OkObjectResult(new ApiResultPaging<BillViewModel>() {
                Items = listBillVm,
                TotalRows = totalRows,
                PageIndex=page,
                PageSize=pageSize,
            });
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] BillViewModel billVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int billId=_billService.Add(billVm);                
                    foreach (var billDetail in billVm.BillDetails)
                    {
                        billDetail.BillId = billId;
                        _billService.AddBillDetail(billDetail);
                    }
                    _billService.SaveChanges();                 
                    return new OkObjectResult(billVm);
                }
                catch(Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }
                           
            }
            return new BadRequestObjectResult(ModelState);
        }

    }
}