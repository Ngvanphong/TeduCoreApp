using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.Data.ViewModels.Identity;
using TeduCoreApp.Data.ViewModels.Permission;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.WebApi.Authorization;
using TeduCoreApp.WebApi.Extensions;
using TeduCoreApp.WebApi.ViewModel;

namespace TeduCoreApp.WebApi.Controllers
{
    public class FunctionController : ApiController
    {
        private IFunctionService _functionService;
        private IPermissionService _permissionService;
        private RoleManager<AppRole> _roleManager;
        private readonly IAuthorizationService _authorizationService;
        public FunctionController(IFunctionService functionService, IPermissionService permissionService,
            RoleManager<AppRole> roleManager, IAuthorizationService authorizationService)
        {
            _functionService = functionService;
            _permissionService = permissionService;
            _roleManager = roleManager;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("getlisthierarchy")]
        public async Task<IActionResult> GetAllHierachy()
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "FUNCTION", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
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

        [HttpGet]
        [Route("getAllPermission")]
        public IActionResult GetAllPermission(string functionId)
        {
            List<AppRole> listRole = _roleManager.Roles.Where(x => x.Name != "Admin").ToList();
            List<PermissionViewModel> listPermissionVm = _permissionService.GetByFunctionId(functionId);
            if (listPermissionVm.Count() == 0)
            {
                foreach (var role in listRole)
                {
                    listPermissionVm.Add(new PermissionViewModel()
                    {
                        RoleId = role.Id,
                        FunctionId = functionId,
                        CanCreate = false,
                        CanRead = false,
                        CanUpdate = false,
                        CanDelete = false,
                        AppRole = new AppRoleViewModel()
                        {
                            Id = role.Id,
                            Name = role.Name,
                            Description = role.Description,
                        }
                    });
                }
            }
            else
            {
                foreach (var role in listRole)
                {
                    if (!listPermissionVm.Any(x => x.RoleId == role.Id))
                    {
                        listPermissionVm.Add(new PermissionViewModel()
                        {
                            RoleId = role.Id,
                            FunctionId = functionId,
                            CanCreate = false,
                            CanRead = false,
                            CanUpdate = false,
                            CanDelete = false,
                            AppRole = new AppRoleViewModel()
                            {
                                Id = role.Id,
                                Name = role.Name,
                                Description = role.Description,
                            }
                        });
                    }
                }
            }
            return new OkObjectResult(listPermissionVm);
        }

        [HttpPost]
        [Route("savePermission")]
        public async Task<IActionResult> SavePermission([FromBody] SavePermissionRequest data)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "FUNCTION", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                _permissionService.DeleteAll(data.FunctionId);
                foreach (var permissionVm in data.Permissions)
                {
                    permissionVm.FunctionId = data.FunctionId;
                    Permission permissionDb = new Permission();
                    permissionDb.UpdatePermission(permissionVm);
                    _permissionService.AddDb(permissionDb);
                }
                List<FunctionViewModel> childFunctions = _functionService.GetAllWithParentId(data.FunctionId);
                if (childFunctions.Count() > 0)
                {
                    foreach (var childFunction in childFunctions)
                    {
                        _permissionService.DeleteAll(childFunction.Id);
                        foreach (var permissionVm in data.Permissions)
                        {
                            permissionVm.FunctionId = childFunction.Id;
                            Permission permissionDb = new Permission();
                            permissionDb.UpdatePermission(permissionVm);
                            _permissionService.AddDb(permissionDb);
                        }
                    }
                }
                _permissionService.SaveChanges();
                return new OkObjectResult("Success");
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> Get(string filter)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "FUNCTION", Operations.Read);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            return new OkObjectResult(_functionService.GetAll(filter));
        }

        [HttpGet]
        [Route("detail/{id}")]
        public IActionResult Detail(string id)
        {
            return new OkObjectResult(_functionService.Get(id));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] FunctionViewModel functionVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "FUNCTION", Operations.Create);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _functionService.Add(functionVm);
                    _functionService.SaveChanges();
                    return new OkObjectResult(functionVm);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex);
                }
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] FunctionViewModel functionVm)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "FUNCTION", Operations.Update);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _functionService.Update(functionVm);
                    _functionService.SaveChanges();
                    return new OkObjectResult(functionVm);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex);
                }
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var hasPermission = await _authorizationService.AuthorizeAsync(User, "FUNCTION", Operations.Delete);
            if (hasPermission.Succeeded == false)
            {
                return new BadRequestObjectResult(CommonConstants.Forbidden);
            }
            _functionService.Delete(id);
            _functionService.SaveChanges();
            return new OkObjectResult(id);
        }
    }
}