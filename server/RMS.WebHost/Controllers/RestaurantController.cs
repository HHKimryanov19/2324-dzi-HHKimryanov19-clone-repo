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
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantServices services;

        public RestaurantController(ApplicationDbContext app, UserManager<ApplicationUser> userManager)
        {
            this.services = new RestaurantServices(app, userManager);
        }

        [Authorize]
        [HttpGet("/restaurants/get-all")]
        public async Task<IResult> GetAllByCity(ICurrentUser currentUser)
        {
            try
            {
                List<RestaurantRM> restaurants = await this.services.GetRestaurants(currentUser.Id);
                return Results.Ok(restaurants);
            }
            catch
            {
                return Results.BadRequest(new List<RestaurantRM>());
            }
        }

        [Authorize]
        [HttpGet("/restaurants/get-single/{restaurantId}")]
        public async Task<IResult> GetRestaurant([FromRoute] Guid restaurantId, ICurrentUser currentUser)
        {
            try
            {
                var restaurant = await this.services.GetRestaurant(restaurantId);
                return Results.Ok(restaurant);
            }
            catch
            {
                return Results.BadRequest(new RestaurantRM());
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("/restaurants/add")]
        public async Task<IResult> AddRestaurant([FromForm] RestaurantIM restaurant)
        {
            try
            {
                var result = await this.services.Add(restaurant);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "add-restaurant-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize(Roles = $"{Roles.Manager},{Roles.Employee},{Roles.Admin}")]
        [HttpPut("/restaurants/update/{restaurantId}")]
        public async Task<IResult> UpdateRestaurant([FromForm] RestaurantIM restaurant, [FromRoute] Guid restaurantId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.Update(restaurant, restaurantId, currentUser.Id);
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
                    Status = "update-restaurant-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("/restaurants/delete/{restaurantId}")]
        public async Task<IResult> DeleteRestaurant([FromRoute] Guid restaurantId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.Delete(restaurantId, currentUser.Id);
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
                    Status = "delete-restaurant-failed",
                    Message = ex.Message,
                });
            }
        }
    }
}