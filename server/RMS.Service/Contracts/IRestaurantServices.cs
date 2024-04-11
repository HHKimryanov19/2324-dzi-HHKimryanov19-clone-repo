using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;

namespace RMS.Service.Contracts
{
    public interface IRestaurantServices
    {
        /// <summary>
        /// Adds new restaurant.
        /// </summary>
        /// <param name="restaurantVM"></param>
        /// <returns>Id of new restaurant.</returns>
        Task<Guid> Add(RestaurantIM restaurantVM);

        /// <summary>
        /// Deletes restaurant.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about deleting is alright, otherwise false.</returns>
        Task<bool> Delete(Guid restaurantId, Guid? userId);

        /// <summary>
        /// Updates restaurant information.
        /// </summary>
        /// <param name="restaurantIM"></param>
        /// <param name="restaurantId"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about updating is alright, otherwise false..</returns>
        Task<bool> Update(RestaurantIM restaurantIM, Guid restaurantId, Guid? userId);

        /// <summary>
        /// Gets restaurant by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Wanted restaurant.</returns>
        Task<RestaurantRM> GetRestaurant(Guid? id);

        /// <summary>
        /// Gets all restaruants in user city.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Collection of restaurants.</returns>
        Task<List<RestaurantRM>> GetRestaurants(Guid? userId);
    }
}
