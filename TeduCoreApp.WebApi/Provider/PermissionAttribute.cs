using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using TeduCoreApp.Data.ViewModels.Permission;
using TeduCoreApp.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace TeduCoreApp.WebApi.Provider
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public string Function;
        public string Action;

        public HttpStatusCode OnAuthorization(AuthorizationFilterContext actionContext)
        {
           
            var principal = actionContext.HttpContext.User as ClaimsPrincipal;

            if (!principal.Identity.IsAuthenticated)
            {
                return HttpStatusCode.Unauthorized;                  
            }
            else
            {
                var roles = JsonConvert.DeserializeObject<List<string>>(principal.FindFirst("roles").Value);
                if (roles.Count > 0)
                {
                    if (!roles.Contains(RoleEnum.Admin.ToString()))
                    {
                        var permissions = JsonConvert.DeserializeObject<List<PermissionViewModel>>(principal.FindFirst("permissions").Value);
                        if (!permissions.Exists(x => x.FunctionId == Function && x.CanCreate) && Action == ActionEnum.Create.ToString())
                        {
                            return HttpStatusCode.Forbidden;
                        }
                        else if (!permissions.Exists(x => x.FunctionId == Function && x.CanRead) && Action == ActionEnum.Read.ToString())
                        {
                            return HttpStatusCode.Forbidden;
                        }
                        else if (!permissions.Exists(x => x.FunctionId == Function && x.CanDelete) && Action == ActionEnum.Delete.ToString())
                        {
                            return HttpStatusCode.Forbidden;
                        }
                        else if (!permissions.Exists(x => x.FunctionId == Function && x.CanUpdate) && Action == ActionEnum.Update.ToString())
                        {
                            return HttpStatusCode.Forbidden;
                        }
                    }
                }
                else
                {
                  return HttpStatusCode.Forbidden;
                }
            }
            return HttpStatusCode.OK;
        }
    }
}