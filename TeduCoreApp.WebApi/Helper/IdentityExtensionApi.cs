using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TeduCoreApp.WebApi.Helper
{
    public static class IdentityExtensionApi
    {
        public static string GetSpecialClaimApi(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }
        
    }
}
