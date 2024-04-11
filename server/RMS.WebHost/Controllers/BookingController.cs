using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;
using RMS.Data;
using RMS.Data.Models;
using RMS.Service.Identity.Services;
using RMS.Service.Implementations;
using RMS.Shared;
using RMS.Shared.Contracts;

namespace RMS.WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingServices services;

        public BookingController(ApplicationDbContext app, UserManager<ApplicationUser> userManager)
        {
            this.services = new BookingServices(app, userManager);
        }

        [Authorize(Roles = Roles.User)]
        [HttpGet("/bookings/get-allByUserId")]
        public async Task<IResult> GetAllByUserId(ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.GetAllByUserId(currentUser.Id);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest(new List<BookingRM>());
            }
        }

        [Authorize(Roles = $"{Roles.Manager},{Roles.Employee}")]
        [HttpGet("/bookings/get-allByRestaurantId/{restaurantId?}")]
        public async Task<IResult> GetAllByRestaurantId(ICurrentUser currentUser, [FromRoute] Guid restaurantId)
        {
            try
            {
                var result = await this.services.GetAllByRestaurantId(currentUser.Id, restaurantId);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest(new List<BookingRM>());
            }
        }

        [Authorize]
        [HttpGet("/booking/get-single/{bookingId}")]
        public async Task<IResult> GetBooking([FromRoute] Guid bookingId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.GetBooking(bookingId, currentUser.Id);
                return Results.Ok(result);
            }
            catch
            {
                return Results.BadRequest(new BookingRM());
            }
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost("/bookings/add/{restaurantId}")]
        public async Task<IResult> Add([FromRoute] Guid restaurantId, [FromBody] BookingIM booking, ICurrentUser currentUser)
        {
            try
            {
                if (this.CheckEntity(booking))
                {
                    var result = await this.services.Add(booking, restaurantId, currentUser.Id);
                    return Results.Ok(result);
                }
                else
                {
                    return Results.BadRequest(new
                    {
                        Status = "add-booking-failed",
                    });
                }
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "add-booking-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize]
        [HttpPut("/bookings/update/{bookingId}")]
        public async Task<IResult> Update([FromBody] BookingIM booking, [FromRoute] Guid bookingId, ICurrentUser currentUser)
        {
            try
            {
                if (this.CheckEntity(booking))
                {
                    var result = await this.services.Update(booking, bookingId, currentUser.Id);
                    if (result)
                    {
                        return Results.Ok();
                    }
                    else
                    {
                        return Results.BadRequest(new
                        {
                            Status = "add-booking-failed",
                        });
                    }
                }
                else
                {
                    return Results.BadRequest(new
                    {
                        Status = "add-booking-failed",
                    });
                }
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    Status = "update-booking-failed",
                    Message = ex.Message,
                });
            }
        }

        [Authorize]
        [HttpDelete("/bookings/delete/{bookingId}")]
        public async Task<IResult> Delete([FromRoute] Guid bookingId, ICurrentUser currentUser)
        {
            try
            {
                var result = await this.services.Delete(bookingId, currentUser.Id);
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
                    Status = "delete-booking-failed",
                    Message = ex.Message,
                });
            }
        }

        [NonAction]
        private bool CheckEntity(BookingIM booking)
        {
            if (booking is null)
            {
                return false;
            }

            if (booking.NumberOfPeople < 0 || booking.NumberOfPeople > 1000)
            {
                return false;
            }

            if (booking.Date < DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}
