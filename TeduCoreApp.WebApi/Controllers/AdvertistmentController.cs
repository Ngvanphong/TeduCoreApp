using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
    
    public class AdvertistmentController : ApiController
    {
        private IAdvertistmentService _advertistmentService;
        private  IHostingEnvironment _env;
        private readonly IAuthorizationService _authorizationService;
        public AdvertistmentController(IAdvertistmentService advertistmentService, IHostingEnvironment env, IAuthorizationService authorizationService)
        {
            _advertistmentService = advertistmentService;
            _env = env;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("getallpaging")]
        public async Task<IActionResult> Get(int page,int pageSize, string filter)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ADVERTISMENT", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            List<AdvertistmentViewModel> listAdvertistmentVm = _advertistmentService.GetAll(page, pageSize, filter, out int totalRows);

            return new OkObjectResult(new ApiResultPaging<AdvertistmentViewModel>()
            {
                Items = listAdvertistmentVm,
                PageIndex=page,
                PageSize=pageSize,
                TotalRows=totalRows,
            });
        }

        [HttpGet]
        [Route("getpage")]
        public async Task<IActionResult> GetPage()
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ADVERTISMENT", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_advertistmentService.GetAllPage());
        }

        [HttpGet]
        [Route("getposition")]
        public async Task<IActionResult> GetPosition()
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ADVERTISMENT", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_advertistmentService.GetAllPosition());
        }

        [HttpGet]
        [Route("detail/{id}")]
        public IActionResult Detail(int id)
        {           
            return new OkObjectResult(_advertistmentService.Detail(id));
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] AdvertistmentViewModel advertistmentVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ADVERTISMENT", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    advertistmentVm.DateCreated = DateTime.Now;
                    _advertistmentService.Add(advertistmentVm);
                    _advertistmentService.SaveChanges();
                    return new OkObjectResult(advertistmentVm);
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
        public async Task<IActionResult> Update([FromBody] AdvertistmentViewModel advertistmentVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ADVERTISMENT", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Advertistment advertistmentDb = _advertistmentService.DetailDb(advertistmentVm.Id);
                    string oldPath =advertistmentDb.Image;
                    if (oldPath != advertistmentVm.Image && !string.IsNullOrEmpty(oldPath))
                    {
                        oldPath.DeletementByString(_env);
                    }
                    advertistmentDb.UpdateAdvertistment(advertistmentVm);
                    _advertistmentService.UpdateDb(advertistmentDb);
                    _advertistmentService.SaveChanges();
                    return new OkObjectResult(advertistmentVm);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }

            }
            return new BadRequestObjectResult(ModelState);
        }


        [HttpPost]
        [Route("addpage")]
        public async Task<IActionResult> AddPage([FromBody] AdvertistmentPageViewModel advertistmentPageVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ADVERTISMENT", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _advertistmentService.AddPageName(advertistmentPageVm);
                    _advertistmentService.SaveChanges();
                    return new OkObjectResult(advertistmentPageVm);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }

            }
            return new BadRequestObjectResult(ModelState);
        }

        
        [HttpPost]
        [Route("addposition")]
        public async Task<IActionResult> AddPosition([FromBody] AdvertistmentPositionViewModel advertistmentPositionVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ADVERTISMENT", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _advertistmentService.AddPosition(advertistmentPositionVm);
                    _advertistmentService.SaveChanges();
                    return new OkObjectResult(advertistmentPositionVm);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }

            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ADVERTISMENT", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            try
            {
                string pathImage = _advertistmentService.DetailDb(id).Image;
                _advertistmentService.Delete(id);
                _advertistmentService.SaveChanges();
                if (!string.IsNullOrEmpty(pathImage))
                {
                    pathImage.DeletementByString(_env);
                }
                return new OkObjectResult(id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletePage")]
        public async Task<IActionResult> DeletePage(string id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ADVERTISMENT", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            try
            {
                _advertistmentService.DeletePageName(id);
                _advertistmentService.SaveChanges();
                return new OkObjectResult(id);
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletePosition")]
        public async Task<IActionResult> DeletePosition(string id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "ADVERTISMENT", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            try
            {
                _advertistmentService.DeletePositionName(id);
                _advertistmentService.SaveChanges();
                return new OkObjectResult(id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}