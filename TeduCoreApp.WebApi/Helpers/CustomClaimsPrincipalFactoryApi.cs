using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.WebApi.Helpers
{
    public class CustomClaimsPrincipalFactoryApi : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        UserManager<AppUser> _userManger;
        public CustomClaimsPrincipalFactoryApi(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
            _userManger = userManager;

        }

        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManger.GetRolesAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("fullName",user.FullName),
                new Claim("avatar",user.Avatar??string.Empty),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
                new Claim("userId",user.Id.ToString())
            });
            return principal;
        }
    }
    
}
