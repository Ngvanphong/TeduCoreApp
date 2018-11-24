using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Application.Implementation
{
   public class AppUserService : IAppUserService
    {
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public AppUserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManger, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManger;
            _context = context;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task RemoveRolesFromUserCustom(string userId, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roleIds = _roleManager.Roles.Where(x => roles.Contains(x.Name)).Select(x => x.Id).ToList();
            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>();
            foreach (var roleId in roleIds)
            {
                userRoles.Add(new IdentityUserRole<Guid> { RoleId = roleId, UserId = user.Id });
            }
            _context.UserRoles.RemoveRange(userRoles);
            await _context.SaveChangesAsync();

        }
    }
}
