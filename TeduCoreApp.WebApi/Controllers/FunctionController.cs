using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.FunctionVm;

namespace TeduCoreApp.WebApi.Controllers
{
    public class FunctionController : ApiController
    {

        private IFunctionService _functionService;
        private IPermissionService _permissionService;

        public FunctionController(IFunctionService functionService, IPermissionService permissionService )
        {
            _functionService = functionService;
            _permissionService = permissionService;
   
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getlisthierarchy")]
        public ActionResult GetAllHierachy()
        {
          
            List<FunctionViewModel> modelVm;
            if (User.IsInRole("Admin"))
            {
                modelVm = _functionService.GetAll(string.Empty);
            }
            else
            {
                modelVm = null;
            }

            List<FunctionViewModel> parents = modelVm.FindAll(x => x.ParentId == null);
            foreach (var parent in parents)
            {
                parent.ChildFunctions = modelVm.Where(x => x.ParentId == parent.Id).ToList();
            }
            return new OkObjectResult(parents);
        }
    }
}