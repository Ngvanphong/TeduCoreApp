
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
    public class FunctionController : ApiController
    {
        private IFunctionService _functionService;
        private IPermissionService _permissionService;
      
        public FunctionController(IFunctionService functionService, IPermissionService permissionService 
           )
        {        
            _functionService = functionService;
            _permissionService = permissionService;           
        }

        [HttpGet]
        [Route("getlisthierarchy")]
        public  ActionResult GetAllHierachy()
        {
            
            string userId = User.GetSpecialClaimsApi("Id");
            
            List<FunctionViewModel> funtionVm;         
            if (User.CheckIsAdminApi())
            {
                funtionVm = _functionService.GetAll(string.Empty);
            }
            else
            {
                funtionVm = _functionService.GetAllWithPermission(userId);
            }

            List<FunctionViewModel> parents = funtionVm.FindAll(x => x.ParentId == null);
            foreach (var parent in parents)
            {
                parent.ChildFunctions = funtionVm.Where(x => x.ParentId == parent.Id).ToList();
            }
            return new OkObjectResult(parents);
        }
    }
}