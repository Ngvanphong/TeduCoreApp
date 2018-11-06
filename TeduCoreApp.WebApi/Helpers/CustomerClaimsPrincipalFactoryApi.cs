using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Permission;

namespace TeduCoreApp.WebApi.Helpers
{
    public class CustomerClaimsPrincipalFactoryApi: UserClaimsPrincipalFactory<AppUser>
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private IPermissionService _permissionService;

        public CustomerClaimsPrincipalFactoryApi(UserManager<AppUser> userManager, IOptions<IdentityOptions> option,
            RoleManager<AppRole> roleManager, IPermissionService permissionService)
            : base(userManager, option)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionService = permissionService;
        }
        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var role = await _userManager.GetRolesAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var permissionViewModels = new List<PermissionViewModel>();
            try
            {
                permissionViewModels = _permissionService.GetByUserId(user.Id).ToList();
            }
            catch
            {

            }
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim("fullName", user.FullName??string.Empty),
                new Claim("avatar", user.Avatar??string.Empty),
                new Claim("email", user.Email),
                new Claim("username",user.UserName.ToString()),
                new Claim("roles", JsonConvert.SerializeObject(roles)??string.Empty),
                new Claim("permissions", JsonConvert.SerializeObject(permissionViewModels)??string.Empty)
            });
            return principal;

        }
    }
}
