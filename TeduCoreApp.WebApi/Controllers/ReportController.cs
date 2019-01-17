using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Dapper.Interfaces;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.WebApi.Authorization;

namespace TeduCoreApp.WebApi.Controllers
{
  
    public class ReportController : ApiController
    {
        private IReportService _reportService;
        private readonly IAuthorizationService _authorizationService;
        public ReportController(IReportService reportService, IAuthorizationService authorizationService)
        {
            _reportService = reportService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("revenue")]
        public async Task<IActionResult> GetRevenue(string fromDate, string toDate)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(await _reportService.GetReportAsync(fromDate, toDate));
        }
    }
}