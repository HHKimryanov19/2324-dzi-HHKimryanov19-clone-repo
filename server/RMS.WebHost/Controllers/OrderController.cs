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
    public class OrderController : ControllerBase
    {
        private readonly OrderServices services;

        public OrderController(ApplicationDbContext app, UserManager<ApplicationUser> userManager)
        {
            this.services = new OrderServices(app, userManager);
        }

        [Authorize]
        [HttpGet("/orders/get/{orderId}")]
        public async Task<IResult> GetOrder([FromRoute] Guid orderId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.GetOrder(orderId, currentUser.Id);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest(new OrderRM());
            }
        }

        [Authorize(Roles = Roles.User)]
        [HttpGet("/orders/get-ByUserId")]
        public async Task<IResult> GetOrdersByUserId(ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.GetOrdersByUserId(currentUser.Id);
                return Results.Ok(result.OrderBy(o => o.Status));
            }
            catch
            {
                return Results.BadRequest(new List<OrderRM>());
            }
        }

        [Authorize]
        [HttpGet("/orders/get-ByRestaurantId")]
        public async Task<IResult> GetOrdersByRestaurantId(ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.GetOrdersByRestaurantId(currentUser.Id);
                return Results.Ok(result.OrderBy(o => o.Status));
            }
            catch
            {
                return Results.BadRequest(new List<OrderRM>());
            }
        }

        [Authorize(Roles = Roles.User)]
        [HttpGet("/orders/get-ByUserRestaurantId/{restaurantId}")]
        public async Task<IResult> GetOrdersByUserRestaurantId([FromRoute] Guid restaurantId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.GetOrdersByUserRestaurantId(restaurantId, currentUser.Id);
                return Results.Ok(result.OrderBy(o => o.Status));
            }
            catch
            {
                return Results.BadRequest(new List<OrderRM>());
            }
        }

        [Authorize]
        [HttpGet("/orders/getOrderDishes/{orderId}")]
        public async Task<IResult> GetOrderDishes([FromRoute] Guid orderId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.GetOrderDishes(orderId, currentUser.Id);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest(new OrderRM());
            }
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost("/orders/assignDish/{dishId}")]
        public async Task<IResult> AssignDish([FromRoute] Guid dishId, [FromBody] int count, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.AssignDish(dishId, count, currentUser.Id);
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
                    Status = "assign-dish-to-order-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize(Roles = Roles.User)]
        [HttpDelete("/orders/removeDish/{orderId}/{dishId}")]
        public async Task<IResult> RemoveDish([FromRoute] Guid orderId, [FromRoute] Guid dishId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.RemoveDish(orderId, dishId, currentUser.Id);
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
                    Status = "remove-dish-from-order-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize(Roles = Roles.User)]
        [HttpPut("/orders/updateCount/{orderId}/{dishId}")]
        public async Task<IResult> UpdateCount([FromRoute] Guid orderId, [FromRoute] Guid dishId, [FromBody] int count, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.UpdateCount(orderId, dishId, count, currentUser.Id);
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
                    Status = "update-dish-count-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize]
        [HttpPut("/orders/update/{orderId}")]
        public async Task<IResult> Update([FromBody] OrderIM order, [FromRoute] Guid orderId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.Update(order, orderId, currentUser.Id);
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
                    Status = "update-order-failed",
                    Message = ex.Message,
                });
            }
        }
    }
}
