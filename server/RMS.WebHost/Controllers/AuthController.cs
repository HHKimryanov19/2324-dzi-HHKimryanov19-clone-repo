using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMS.Shared.Models.InputModels;
using RMS.Data;
using RMS.Service.Implementations;
using RMS.Shared;
using RMS.Shared.Contracts;
using RMS.Shared.Models;

namespace RMS.WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly UserServices userServices;
        private readonly AuthServices authServices;
        private readonly IConfiguration config;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext app, IConfiguration config)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userServices = new UserServices(app, userManager);
            this.authServices = new AuthServices(app, userManager, roleManager, config);
            this.config = config;
        }

        [HttpPost("/auth/user-login")]
        public async Task<IResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var token = await authServices.Login(loginModel);

                return Results.Ok(token);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "user-login-failed",
                    Message = ex.Message,
                });
            }
        }

        [HttpPost("/auth/user-register")]
        public async Task<IResult> Register([FromBody] UserIM userIM)
        {
            await this.authServices.SeedRoles();
            await this.authServices.AdminSeed();

            try
            {
                var result = await this.authServices.Register(userIM);

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "user-register-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize]
        [HttpGet("/auth/user-role")]
        public async Task<IResult> GetRole(ICurrentUser user)
        {
            try
            {
                var appUser = await this.userManager.FindByIdAsync(user.Id.ToString());
                var role = await this.userManager.GetRolesAsync(appUser);
                int roleCode = 0;

                if (role.Contains(Roles.Admin))
                {
                    roleCode = 0;
                }

                if (role.Contains(Roles.User))
                {
                    roleCode = 1;
                }

                if (role.Contains(Roles.Employee))
                {
                    roleCode = 2;
                }

                if (role.Contains(Roles.Manager))
                {
                    roleCode = 3;
                }

                if (role.Contains(Roles.Deliver))
                {
                    roleCode = 4;
                }

                return Results.Ok(roleCode);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "get-role-failed",
                    Message = ex.Message,
                });
            }
        }
    }
}
