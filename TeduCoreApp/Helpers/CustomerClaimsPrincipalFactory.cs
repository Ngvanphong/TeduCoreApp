using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TeduCoreApp.Data.Entities;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace TeduCoreApp.Helpers
{
    public class CustomerClaimsPrincipalFactory: UserClaimsPrincipalFactory<AppUser>
    {
        private UserManager<AppUser> _userManager;
        public CustomerClaimsPrincipalFactory(UserManager<AppUser> userManager, IOptions<IdentityOptions> option)
            :base(userManager,option)
        {
            _userManager = userManager;
        }
        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var role = await _userManager.GetRolesAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim("UserName",user.UserName),
                new Claim("Email",user.Email),
                new Claim("FullName",user.FullName??string.Empty),
                new Claim("Avatar",user.Avatar??string.Empty),
            });
            return principal;

        }
    }
}
