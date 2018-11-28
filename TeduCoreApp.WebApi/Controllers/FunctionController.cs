using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.FunctionVm;
using TeduCoreApp.Data.ViewModels.Identity;
using TeduCoreApp.Data.ViewModels.Permission;
using TeduCoreApp.WebApi.Extensions;
using TeduCoreApp.WebApi.ViewModel;

namespace TeduCoreApp.WebApi.Controllers
{
    public class FunctionController : ApiController
    {
        private IFunctionService _functionService;
        private IPermissionService _permissionService;
        private RoleManager<AppRole> _roleManager;

        public FunctionController(IFunctionService functionService, IPermissionService permissionService,
            RoleManager<AppRole> roleManager)
        {
            _functionService = functionService;
            _permissionService = permissionService;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("getlisthierarchy")]
        public ActionResult GetAllHierachy()
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
        public IActionResult SavePermission([FromBody] SavePermissionRequest data)
        {
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
        public IActionResult Get(string filter)
        {
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
        public IActionResult Add([FromBody] FunctionViewModel functionVm)
        {
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
        public IActionResult Update([FromBody] FunctionViewModel functionVm)
        {
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
        public IActionResult Delete(string id)
        {
            _functionService.Delete(id);
            _functionService.SaveChanges();
            return new OkObjectResult(id);
        }
    }
}