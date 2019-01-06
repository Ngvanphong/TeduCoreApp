using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Pantner;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{ 
    public class PantnerController : ApiController
    {
        private IPantnerService _pantnerService;
        private IHostingEnvironment _env;
        public PantnerController(IPantnerService pantnerService, IHostingEnvironment env)
        {
            _pantnerService = pantnerService;
            _env = env;
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult Get()
        {
            return new OkObjectResult(_pantnerService.GetAll());
        }

        [HttpGet]
        [Route("detail/{id:int}")]
        public IActionResult Detail(int id)
        {
            return new OkObjectResult(_pantnerService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] PantnerViewModel pantnerVm)
        {
            if (ModelState.IsValid)
            {
                _pantnerService.Add(pantnerVm);
                _pantnerService.SaveChanges();
                return new OkObjectResult(pantnerVm);
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] PantnerViewModel pantnerVm)
        {
            if (ModelState.IsValid)
            {
                Pantner pantnerDb = _pantnerService.GetByIdDb(pantnerVm.Id);
                string oldPath = pantnerDb.Image;
                if (oldPath != pantnerVm.Image && !string.IsNullOrEmpty(oldPath))
                {
                    oldPath.DeletementByString(_env);
                }
                pantnerDb.UpdatePantner(pantnerVm);
                _pantnerService.Update(pantnerDb);
                _pantnerService.SaveChanges();
                return new OkObjectResult(pantnerVm);

            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            string oldPath = _pantnerService.GetByIdDb(id).Image;
            if (!string.IsNullOrEmpty(oldPath))
            {
                oldPath.DeletementByString(_env);
            }
            _pantnerService.Delete(id);
            _pantnerService.SaveChanges();
            return new OkObjectResult(id);

        }
    }
}