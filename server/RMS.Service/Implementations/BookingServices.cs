using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;
using RMS.Data;
using RMS.Data.Models;
using RMS.Service.Contracts;
using RMS.Service.Identity.Services;
using RMS.Shared;
using RMS.Shared.Contracts;

namespace RMS.Service.Implementations
{
    public class BookingServices : IBookingServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        /// <inheritdoc />
        public async Task<Guid> Add(BookingIM booking, Guid restaurantId, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var restaurant = await this._context.Restaurants.FindAsync(restaurantId);
            if (restaurant is null)
            {
                throw new Exception(ExceptionMessages.RestaurantMassage);
            }

            var newBooking = booking.Adapt<Booking>();
            newBooking.UserId = userId;
            newBooking.RestaurantId = restaurantId;

            await this._context.Bookings.AddAsync(newBooking);
            await this._context.SaveChangesAsync();

            return newBooking.Id;
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid bookingId, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            Booking? booking = await this._context.Bookings.FindAsync(bookingId);
            if (booking is null)
            {
                throw new Exception(ExceptionMessages.BookingMessage);
            }

            if (booking.UserId != userId)
            {
                return false;
            }

            this._context.Bookings.Remove(booking);
            await this._context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<List<BookingRM>> GetAllByUserId(Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var bookings = await this._context.Bookings.Include(b => b.User).Include(b => b.Restaurant).Where(b => b.UserId == userId).ToListAsync();

            if (bookings is null)
            {
                throw new Exception(ExceptionMessages.BookingMessage);
            }

            return this.MapBooking(bookings);
        }

        /// <inheritdoc />
        public async Task<List<BookingRM>> GetAllByRestaurantId(Guid? userId, Guid? restaurantId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            if (restaurantId == Guid.Empty)
            {
                if (user.RestaurantId is not null)
                {
                    restaurantId = user.RestaurantId;
                }
            }
            else
            {
                var restaurant = await this._context.Restaurants.FindAsync(restaurantId);
                if (restaurant is null)
                {
                    throw new Exception(ExceptionMessages.RestaurantMassage);
                }
            }

            var bookings = await this._context.Bookings
                .Include(b => b.User)
                .Include(b => b.Restaurant)
                .Where(b => b.RestaurantId == restaurantId).ToListAsync();

            if (bookings is null)
            {
                throw new Exception(ExceptionMessages.BookingMessage);
            }

            return this.MapBooking(bookings);
        }

        /// <inheritdoc />
        public async Task<BookingRM> GetBooking(Guid bookingId, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var booking = await this._context.Bookings.FindAsync(bookingId);
            if (booking is null)
            {
                throw new Exception(ExceptionMessages.BookingMessage);
            }

            var roles = await this._userManager.GetRolesAsync(user);

            if (roles.Contains(Roles.User))
            {
                if (booking.UserId != userId)
                {
                    throw new Exception();
                }

                return booking.Adapt<BookingRM>();
            }

            if (roles.Contains(Roles.Employee) || roles.Contains(Roles.Manager))
            {
                if (booking.RestaurantId != user.RestaurantId)
                {
                    throw new Exception();
                }

                return booking.Adapt<BookingRM>();
            }

            return new BookingRM();
        }

        /// <inheritdoc />
        public async Task<bool> Update(BookingIM bookingIM, Guid bookingId, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var booking = await this._context.Bookings.FindAsync(bookingId);
            if (booking is null)
            {
                throw new Exception(ExceptionMessages.BookingMessage);
            }

            bool isAllowed = false;
            var roles = await this._userManager.GetRolesAsync(user);

            if (roles.Contains(Roles.User))
            {
                if (booking.UserId == userId)
                {
                    isAllowed = true;
                }
            }

            if (roles.Contains(Roles.Employee) || roles.Contains(Roles.Manager))
            {
                if (booking.RestaurantId == user.RestaurantId)
                {
                    isAllowed = true;
                }
            }

            if (isAllowed)
            {
                booking.Date = bookingIM.Date;
                booking.IsInside = bookingIM.IsInside;
                booking.NumberOfPeople = bookingIM.NumberOfPeople;

                this._context.Bookings.Update(booking);
                await this._context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Maps matching properties and user and restaurant names.
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="userId"></param>
        /// <returns>Collection of mapped bookings.</returns>
        private List<BookingRM> MapBooking(List<Booking> bookings)
        {
            List<BookingRM> mapBookings = bookings.Adapt<List<BookingRM>>();
            for (int i = 0; i < bookings.Count; i++)
            {
                mapBookings[i].FullUserName = bookings[i]?.User?.FirstName + " " + bookings[i]?.User?.LastName;
                mapBookings[i].RestaurantName = bookings[i]?.Restaurant?.Name;
            }

            return mapBookings;
        }
    }
}
