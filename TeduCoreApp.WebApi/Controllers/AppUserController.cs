using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
    public class AppUserController : ApiController
    {
        private UserManager<AppUser> _userManager;
        private IMapper _mapper;
        private IHostingEnvironment _env;

        public AppUserController(UserManager<AppUser> userManager, IMapper mapper, IHostingEnvironment env)
        {
            _userManager = userManager;
            _mapper = mapper;
            _env = env;
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
                    var result = await _userManager.CreateAsync(appUser);
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
                    string oldPath = appUser.Avatar;
                    AppUser newAppUser = _mapper.Map<AppUser>(appUserVm);
                    var result = await _userManager.UpdateAsync(newAppUser);
                    if (result.Succeeded)
                    {
                        if (oldPath != appUserVm.Avatar && !string.IsNullOrEmpty(oldPath))
                        {
                            DeleteElementImage(oldPath);
                        }
                        var roles = await _userManager.GetRolesAsync(newAppUser);
                        await _userManager.RemoveFromRolesAsync(newAppUser, roles);
                        var newRoles = appUserVm.Roles.ToArray();
                        newRoles = newRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(newAppUser, newRoles);
                        return new OkObjectResult(appUserVm);
                    }
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
                DeleteElementImage(appUser.Avatar);
            }
            await _userManager.DeleteAsync(appUser);
            return new OkObjectResult(id);
        }

        public void DeleteElementImage(string path)
        {
            string webHost = _env.WebRootPath;
            string fullPath = webHost + path;
            System.IO.File.Delete(fullPath);
        }
    }
}