using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
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

        [HttpGet]
        [Route("detail/{id}")]
        public IActionResult Detail(int id)
        {
            return new OkObjectResult(_billService.GetDetail(id));
        }

        [HttpGet]
        [Route("getalldetails/{id}")]
        public IActionResult DetaiBillDetail(int id)
        {
            return new OkObjectResult(_billService.GetBillDetails(id));
        }



        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] BillViewModel billVm)
        {
            if (ModelState.IsValid)
            {
                try
                {               
                    var listBillDetails = new List<BillDetailViewModel>();
                    foreach (var item in billVm.BillDetails)
                    {
                        listBillDetails.Add(new BillDetailViewModel()
                        {                           
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            SizeId = item.SizeId,
                            ColorId=item.ColorId,
                        });
                    }
                    billVm.BillDetails = listBillDetails;
                    _billService.Add(billVm);
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

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            _billService.DeleteBill(id);
            List<BillDetailViewModel> listBillDetail = _billService.GetBillDetails(id);
            foreach(var item in listBillDetail)
            {
                _billService.DeleteBillDetail(item.Id);
            }
            _billService.SaveChanges();
            return new OkObjectResult(id);
        }

    }
}