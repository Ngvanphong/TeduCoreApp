using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Advertistment;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
    
    public class AdvertistmentController : ApiController
    {
        private IAdvertistmentService _advertistmentService;
        private  IHostingEnvironment _env;
        public AdvertistmentController(IAdvertistmentService advertistmentService, IHostingEnvironment env)
        {
            _advertistmentService = advertistmentService;
            _env = env;
        }

        [HttpGet]
        [Route("getallpaging")]
        public IActionResult Get(int page,int pageSize, string filter)
        {
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
        public IActionResult GetPage()
        {
            return new OkObjectResult(_advertistmentService.GetAllPage());
        }

        [HttpGet]
        [Route("getposition")]
        public IActionResult GetPosition()
        {
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
        public IActionResult Add([FromBody] AdvertistmentViewModel advertistmentVm)
        {
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
        public IActionResult Update([FromBody] AdvertistmentViewModel advertistmentVm)
        {
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
        public IActionResult AddPage([FromBody] AdvertistmentPageViewModel advertistmentPageVm)
        {
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
        public IActionResult AddPosition([FromBody] AdvertistmentPositionViewModel advertistmentPositionVm)
        {
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
        public IActionResult Delete(int id)
        {
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
        public IActionResult DeletePage(string id)
        {
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
        public IActionResult DeletePosition(string id)
        {
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