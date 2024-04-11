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
    public class UserController : ControllerBase
    {
        private readonly UserServices services;

        public UserController(ApplicationDbContext app, UserManager<ApplicationUser> userManager)
        {
            this.services = new UserServices(app, userManager);
        }

        [Authorize]
        [HttpGet("/users/info")]
        public async Task<IResult> GetUserInfo(ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.GetUser(currentUser.Id);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "get-user-info-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize]
        [HttpPut("/users/update")]
        public async Task<IResult> Update([FromBody]UserIM userInfo, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.Update(userInfo, currentUser.Id);
                if (result)
                {
                    return Results.Ok();
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "update-user-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize]
        [HttpPut("/users/changePassword")]
        public async Task<IResult> ChnagePassord([FromBody] PasswordsModel passwords, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.ChangePassword(passwords, currentUser.Id);
                if (result)
                {
                    return Results.Ok();
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "change-password-user-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize]
        [HttpDelete("/users/delete")]
        public async Task<IResult> Delete([FromBody] string email, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.Delete(email, currentUser.Id);
                if (result)
                {
                    return Results.Ok();
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "delete-user-failed",
                    Message = ex.Message,
                });
            }
        }
    }
}
