using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;
using RMS.Data;
using RMS.Service.Implementations;
using RMS.Shared;
using RMS.Shared.Contracts;

namespace RMS.WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishController : ControllerBase
    {
        private readonly DishServices services;

        public DishController(ApplicationDbContext app, UserManager<ApplicationUser> userManager)
        {
            this.services = new DishServices(app, userManager);
        }

        [Authorize]
        [HttpGet("/dishes/get-all/{restaurantId?}")]
        public async Task<IResult> GetAll(ICurrentUser currentUser, [FromRoute] Guid restaurantId, [FromQuery] int category = 0)
        {
            try
            {
                var result = await this.services.GetAll(currentUser.Id, restaurantId, category);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest(new List<DishRM>());
            }
        }

        [Authorize]
        [HttpGet("/dishes/get-single/{dishId}")]
        public async Task<IResult> GetDish([FromRoute] Guid dishId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.GetDish(dishId);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "get-dish-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize(Roles = $"{Roles.Manager},{Roles.Employee}")]
        [HttpPost("/dishes/add")]
        public async Task<IResult> AddDish([FromForm] DishIM dish, ICurrentUser currentUser)
        {
            try
            {
                if (this.CheckEntity(dish))
                {
                    var result = await this.services.Add(dish, currentUser.Id);
                    return Results.Ok(result);
                }
                else
                {
                    return Results.BadRequest(new
                    {
                        Status = "add-dish-failed",
                    });
                }
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "add-dish-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize(Roles = $"{Roles.Manager},{Roles.Employee}")]
        [HttpPut("/dishes/update/{dishId}")]
        public async Task<IResult> UpdateDish([FromForm] DishIM dishIM, [FromRoute] Guid dishId, ICurrentUser currentUser)
        {
            try
            {
                if (this.CheckEntity(dishIM))
                {
                    var result = await this.services.Update(dishIM, dishId, currentUser.Id);
                    if (result)
                    {
                        return Results.Ok();
                    }
                    else
                    {
                        return Results.BadRequest(new
                        {
                            Status = "update-dish-failed",
                        });
                    }
                }
                else
                {
                    return Results.BadRequest(new
                    {
                        Status = "update-dish-failed",
                    });
                }
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "update-dish-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize(Roles = $"{Roles.Manager},{Roles.Employee}")]
        [HttpDelete("/dishes/delete/{dishId}")]
        public async Task<IResult> DeleteDish([FromRoute] Guid dishId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.Delete(dishId, currentUser.Id);
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
                    Status = "delete-dish-failed",
                    Message = ex.Message,
                });
            }
        }

        [NonAction]
        private bool CheckEntity(DishIM dish)
        {
            if (dish is null)
            {
                return false;
            }


            if (dish.Title is null && dish.Title != string.Empty && dish?.Title?.Length >= 150)
            {
                return false;
            }

            if (dish?.Info is null && dish?.Info?.Length > 300 && dish.Info != string.Empty)
            {
                return false;
            }

            return true;
        }
    }
}
