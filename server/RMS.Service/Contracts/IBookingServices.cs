using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;
using RMS.Data.Models;

namespace RMS.Service.Contracts
{
    public interface IBookingServices
    {
        /// <summary>
        /// Add new booking for user.
        /// </summary>
        /// <param name="booking"></param>
        /// <param name="restaurantId"></param>
        /// <param name="userId"></param>
        /// <returns>Id of new booking.</returns>
        Task<Guid> Add(BookingIM booking, Guid restaurantId, Guid? userId);

        /// <summary>
        /// Deletes booking.
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about deleting is alright, otherwise false.</returns>
        Task<bool> Delete(Guid bookingId, Guid? userId);

        /// <summary>
        /// Updates booking information.
        /// </summary>
        /// <param name="bookingIM"></param>
        /// <param name="bookingId"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about updating is alright, otherwise false.</returns>
        Task<bool> Update(BookingIM bookingIM, Guid bookingId, Guid? userId);

        /// <summary>
        /// Gets all bookings by userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Collection of user's bookings.</returns>
        Task<List<BookingRM>> GetAllByUserId(Guid? userId);

        /// <summary>
        /// Gets all bookings by restaurantId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="restaurantId"></param>
        /// <returns>Collection of restaurant's booking.</returns>
        Task<List<BookingRM>> GetAllByRestaurantId(Guid? userId, Guid? restaurantId = default!);

        /// <summary>
        /// Gets booking by Id.
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="userId"></param>
        /// <returns>Wanted booking.</returns>
        Task<BookingRM> GetBooking(Guid bookingId, Guid? userId);
    }
}