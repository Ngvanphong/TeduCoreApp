using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TeduCoreApp.WebApi.Extensions
{
    public static class GetUserIdentityClaim
    {
        public static string GetSpecialClaimsApi(this ClaimsPrincipal claimsPrincipal, string type)
        {

            ClaimsIdentity listClaimIdentity = claimsPrincipal.Identities.FirstOrDefault();
            foreach(var item in listClaimIdentity.Claims)
            {
                if (item.Type == type) return item.Value;
               
            }
            return string.Empty;
        }

        public static bool CheckIsAdminApi(this ClaimsPrincipal claimsPrincipal)
        {

            ClaimsIdentity listClaimIdentity = claimsPrincipal.Identities.FirstOrDefault();
            string roles = null;
            foreach (var item in listClaimIdentity.Claims)
            {
                if (item.Type == ClaimTypes.Role) roles=item.Value;

            }
            string[] listRole = roles.Split(";");
            foreach(var role in listRole)
            {
                if (role == "Admin") return true;
            }
            return false;
            
        }

    }
}
