using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMS.Data;
using RMS.Service.Implementations;
using RMS.Shared;
using RMS.Shared.Contracts;
using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;

namespace RMS.WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeServices services;

        public EmployeeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext app)
        {
            this.services = new EmployeeServices(userManager, roleManager, app);
        }

        [Authorize(Roles = Roles.Manager)]
        [HttpPost("/employees/addEmployee")]
        public async Task<IResult> AddEmployee([FromBody] string email, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.AddEmployee(email, true, currentUser.Id);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "add-employee-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("/employees/addManager/{restaurantId}")]
        public async Task<IResult> AddManager([FromBody] string email, [FromRoute]Guid restaurantId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.AddManager(email, restaurantId);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "add-manager-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize(Roles = Roles.Manager)]
        [HttpPost("/employees/addDeliver")]
        public async Task<IResult> AddDeliver([FromBody] string email, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.AddEmployee(email, false, currentUser.Id);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "add-deliver-failed",
                    Message = ex.Message,
                });
            }
        }
    }
}
