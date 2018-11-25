using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Identity;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.WebApi.Extensions;

namespace TeduCoreApp.WebApi.Controllers
{
    public class AppUserController : ApiController
    {
        private UserManager<AppUser> _userManager;
        private IMapper _mapper;
        private IHostingEnvironment _env;
        private IAppUserService _appUserService;

        public AppUserController(UserManager<AppUser> userManager, IMapper mapper, IHostingEnvironment env, IAppUserService appUserService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _env = env;
            _appUserService = appUserService;
        }

        [HttpGet]
        [Route("getlistpaging")]
        public IActionResult Get(int pageSize, int page = 1, string filter = "")
        {
            var listUser = _userManager.Users;
            int totalRow = 0;
            if (!string.IsNullOrEmpty(filter))
            {
                listUser = listUser.Where(x => x.UserName.Contains(filter) || x.FullName.Contains(filter));
            }
            totalRow = listUser.Count();
            listUser = listUser.OrderByDescending(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            List<AppUserViewModel> listUserVm = _mapper.Map<List<AppUserViewModel>>(listUser.ToList());
            return new OkObjectResult(new ApiResultPaging<AppUserViewModel>()
            {
                PageIndex = page,
                PageSize = pageSize,
                TotalRows = totalRow,
                Items = listUserVm,
            });
        }

        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            var listRole = await _userManager.GetRolesAsync(appUser);
            AppUserViewModel appUserVm = _mapper.Map<AppUserViewModel>(appUser);
            appUserVm.Roles = listRole;
            return new OkObjectResult(appUserVm);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] AppUserViewModel appUserVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser appUser = _mapper.Map<AppUser>(appUserVm);
                    var result = await _userManager.CreateAsync(appUser, appUserVm.Password);
                    if (result.Succeeded)
                    {
                        var role = appUserVm.Roles.ToArray();
                        await _userManager.AddToRolesAsync(appUser, role);
                        return new OkObjectResult(appUserVm);
                    }
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
        public async Task<IActionResult> Update([FromBody] AppUserViewModel appUserVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser appUser = await _userManager.FindByIdAsync(appUserVm.Id.ToString());
                    var roles = await _userManager.GetRolesAsync(appUser);
                    await _appUserService.RemoveRolesFromUserCustom(appUser.Id.ToString(), roles.ToArray());
                    string oldPath = appUser.Avatar;
                    if (oldPath != appUserVm.Avatar && !string.IsNullOrEmpty(oldPath))
                    {
                        oldPath.DeletementByString(_env);
                    }
                    appUser.UpdateUser(appUserVm);
                    appUser.SecurityStamp = Guid.NewGuid().ToString();
                    await _userManager.UpdateAsync(appUser);
                    var newRoles = appUserVm.Roles.ToArray();
                    newRoles = newRoles ?? new string[] { };
                    await _userManager.AddToRolesAsync(appUser, newRoles);
                    return new OkObjectResult(appUserVm);
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
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (!string.IsNullOrEmpty(appUser.Avatar))
            {
                appUser.Avatar.DeletementByString(_env);
            }
            await _userManager.DeleteAsync(appUser);
            return new OkObjectResult(id);
        }

       
    }
}