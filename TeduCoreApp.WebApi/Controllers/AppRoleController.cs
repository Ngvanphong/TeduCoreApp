using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Identity;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.WebApi.Controllers
{
    public class AppRoleController : ApiController
    {
        private RoleManager<AppRole> _roleManager;
        private IMapper _mapper;

        public AppRoleController(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getlistall")]
        public IActionResult Get()
        {
            List<AppRole> listRoles = _roleManager.Roles.ToList();
            return new OkObjectResult(_mapper.Map<List<AppRoleViewModel>>(listRoles));
        }

        [HttpGet]
        [Route("getlistpaging")]
        public IActionResult Get(int pageSize, int page = 1, string filter = null)
        {
            int totalRows = 0;
            var listRole = _roleManager.Roles;
            if (!string.IsNullOrEmpty(filter))
            {
                listRole = listRole.Where(x => x.Description.Contains(filter) || x.Name.Contains(filter));
            }
            totalRows = listRole.Count();
            listRole = listRole.Skip((page - 1) * pageSize).Take(pageSize);
            List<AppRoleViewModel> listRoleVm = _mapper.Map<List<AppRoleViewModel>>(listRole.ToList());
            return new OkObjectResult(new ApiResultPaging<AppRoleViewModel>()
            {
                PageIndex = page,
                PageSize = pageSize,
                Items = listRoleVm,
                TotalRows = totalRows,
            });
        }

        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            return new OkObjectResult(_mapper.Map<AppRoleViewModel>(appRole));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] AppRoleViewModel appRoleVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppRole appRole = _mapper.Map<AppRole>(appRoleVm);
                    await _roleManager.CreateAsync(appRole);
                    return new OkObjectResult(appRoleVm);
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
        public async Task<IActionResult> Update([FromBody] AppRoleViewModel appRoleVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppRole appRole = _mapper.Map<AppRole>(appRoleVm);
                    await _roleManager.UpdateAsync(appRole);
                    return new OkObjectResult(appRoleVm);
                }
                catch(Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }                            
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                AppRole appRole = await _roleManager.FindByIdAsync(id);              
                await _roleManager.DeleteAsync(appRole);
                return new OkObjectResult(id);
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
           
        }

    }
}