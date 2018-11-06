using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Permission;
using TeduCoreApp.WebApi.Helper;
using TeduCoreApp.WebApi.Models;
using TeduCoreApp.WebApi.Provider;

namespace TeduCoreApp.WebApi.Controllers
{
 
    public class AccountController : ApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IPermissionService _premissionService;
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILoggerFactory loggerFactory, IConfiguration config, IPermissionService premissionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _config = config;
            _premissionService = premissionService;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
       
        public async Task<IActionResult> Login(string userName,string password,bool rememberMe=false)
        {
         
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(userName, password, false, true);
                if (!result.Succeeded)
                {
                    return new BadRequestObjectResult(result.ToString());
                }
                var roles = await _userManager.GetRolesAsync(user);
                var permissionViewModels = new List<PermissionViewModel>();
                try
                {
                   permissionViewModels = _premissionService.GetByUserId(user.Id).ToList();
                }
                catch
                {

                }
                var claims = User.Claims;
                var props = new Dictionary<string,string>
                    {
                        {"fullName", user.FullName},
                        {"avatar", user.Avatar },
                        {"email", user.Email},
                        {"username", user.UserName},
                        {"permissions",JsonConvert.SerializeObject(permissionViewModels) },
                        {"roles",JsonConvert.SerializeObject(roles) }

                    };
                _logger.LogError(_config["Tokens"]);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds);
                _logger.LogInformation(1, "User logged in.");

                return new OkObjectResult(new { token = new JwtSecurityTokenHandler().WriteToken(token), userLogin=props});
            }
            return new BadRequestObjectResult("Login failure");
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(model);
            }

            var user = new AppUser { FullName = "demo", UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // User claim for write customers data
                await _userManager.AddClaimAsync(user, new Claim("Customers", "Write"));

                await _signInManager.SignInAsync(user, false);
                _logger.LogInformation(3, "User created a new account with password.");
                return new OkObjectResult(model);
            }

            return new BadRequestObjectResult(model);
        }
    }
}