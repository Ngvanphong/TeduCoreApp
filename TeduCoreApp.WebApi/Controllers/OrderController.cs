using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.WebApi.Controllers
{
    public class OrderController : ApiController
    {
        private IBillService _billService;
        private UserManager<AppUser> _userManger;

        public OrderController(IBillService billService, UserManager<AppUser> userManger)
        {
            _billService = billService;
            _userManger = userManger;
        }

        [HttpGet]
        [Route("getlistpaging")]
        public IActionResult Get(string startDate, string endDate,
            string customerName, BillStatus billStatus, int pageSize, int page = 1)
        {
            List<BillViewModel> listBillVm = _billService.GetList(startDate, endDate, customerName, billStatus, page, pageSize, out int totalRows);
            return new OkObjectResult(new ApiResultPaging<BillViewModel>()
            {
                Items = listBillVm,
                TotalRows = totalRows,
                PageIndex = page,
                PageSize = pageSize,
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
        public IActionResult Add([FromBody] BillViewModel billVmPost)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BillViewModel billVm = billVmPost;
                    var listBillDetails = new List<BillDetailViewModel>();
                    foreach (var item in billVm.BillDetails)
                    {
                        listBillDetails.Add(new BillDetailViewModel()
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            SizeId = item.SizeId,
                            ColorId = item.ColorId,
                        });
                    }
                    billVm.BillDetails = listBillDetails;
                    int billId = _billService.Add(billVm);
                    billVm.Id = billId;
                    billVm.DateCreated = DateTime.Now;
                    return new OkObjectResult(billVm);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] BillViewModel billVmPost)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(billVmPost.CustomerId.ToString()))
                {
                    _billService.Update(billVmPost);
                    _billService.SaveChanges();
                    return new OkObjectResult(billVmPost.Id);
                }
                else
                {
                    BillStatus billStauts = billVmPost.BillStatus;
                    if (billStauts == BillStatus.Cancelled || billStauts == BillStatus.Returned)
                    {
                        AppUser appUser = await _userManger.FindByIdAsync(billVmPost.CustomerId.ToString());
                        var totalBalance = appUser.Balance;

                        appUser.Balance = appUser.Balance + (decimal)billVmPost.TotalMoneyOrder + (decimal)billVmPost.FeeShipping
                        - (decimal)billVmPost.BalanceForBill - (decimal)billVmPost.TotalMoneyPayment;

                        var result = await _userManger.UpdateAsync(appUser);
                        if (result.Succeeded)
                        {
                            _billService.Update(billVmPost);
                            _billService.SaveChanges();
                            return new OkObjectResult(billVmPost.Id);
                        }
                        else
                        {
                            return new BadRequestObjectResult(ModelState);
                        }
                    }
                    else
                    {
                        _billService.Update(billVmPost);
                        _billService.SaveChanges();
                        return new OkObjectResult(billVmPost.Id);
                    }
                }
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            _billService.DeleteBill(id);
            _billService.SaveChanges();
            return new OkObjectResult(id);
        }
    }
}