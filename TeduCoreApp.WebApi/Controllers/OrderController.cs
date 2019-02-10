using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.ViewModels.Bill;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Authorization;

namespace TeduCoreApp.WebApi.Controllers
{
    public class OrderController : ApiController
    {
        private IBillService _billService;       
        private UserManager<AppUser> _userManger;
        private IProductService _productService;
        private IProductQuantityService _productQuantityService;
        private readonly IAuthorizationService _authorizationService;
        public OrderController(IBillService billService, UserManager<AppUser> userManger, IProductService productService,
            IAuthorizationService authorizationService, IProductQuantityService productQuantityService)
        {
            _billService = billService;
            _userManger = userManger;
            _productService = productService;
            _authorizationService = authorizationService;
            _productQuantityService = productQuantityService;
        }

        [HttpGet]
        [Route("getlistpaging")]
        public async Task<IActionResult> Get(string startDate, string endDate,
            string customerName, BillStatus billStatus, int pageSize, int page = 1)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "BILL", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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
        public async Task<IActionResult> Add([FromBody] BillViewModel billVmPost)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "BILL", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    decimal totalPayment = 0;
                    BillViewModel billVm = billVmPost;
                    var listBillDetails = new List<BillDetailViewModel>();
                    foreach (var item in billVm.BillDetails)
                    {
                        totalPayment = totalPayment + item.Quantity * item.Price;
                        listBillDetails.Add(new BillDetailViewModel()
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            SizeId = item.SizeId,
                            ColorId = item.ColorId,
                            OriginalPrice=item.OriginalPrice
                        });
                    }
                    billVm.TotalMoneyPayment = totalPayment;
                    billVm.TotalMoneyOrder = totalPayment;
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
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "BILL", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                if (billVmPost.BillStatus == BillStatus.Completed)
                {
                    List<BillDetailViewModel> billDetailVm = _billService.GetBillDetails(billVmPost.Id);
                    foreach(var item in billDetailVm)
                    {
                        Product productDb = _productService.GetProductDbById(item.ProductId);
                        productDb.ViewCount = productDb.ViewCount + item.Quantity;                       
                        _productService.UpdateDb(productDb);
                        ProductQuantity productQuantityDb = _productQuantityService.GetSingleDb(item.ProductId, item.SizeId, item.ColorId);
                        productQuantityDb.Quantity = productQuantityDb.Quantity - item.Quantity;
                        _productQuantityService.UpdateDb(productQuantityDb);
                        _billService.SaveChanges();

                    }
                }
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
        public async Task<IActionResult> Delete(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "BILL", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            _billService.DeleteBill(id);
            _billService.SaveChanges();
            return new OkObjectResult(id);
        }
    }
}