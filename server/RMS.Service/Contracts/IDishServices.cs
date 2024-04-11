using Azure;
using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;
using RMS.Data.Models;

namespace RMS.Service.Contracts
{
    public interface IDishServices
    {
        /// <summary>
        /// Adds new dish.
        /// </summary>
        /// <param name="dish"></param>
        /// <param name="userId"></param>
        /// <returns>Id of the new dish.</returns>
        Task<Guid> Add(DishIM dish, Guid? userId);

        /// <summary>
        /// Deletes dish.
        /// </summary>
        /// <param name="dishId"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about deleting is alright, otherwise false.</returns>
        Task<bool> Delete(Guid dishId, Guid? userId);

        /// <summary>
        /// Updates dish information.
        /// </summary>
        /// <param name="dishVM"></param>
        /// <param name="dishId"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about updating is alright, otherwise false.</returns>
        Task<bool> Update(DishIM dishVM, Guid dishId, Guid? userId);

        /// <summary>
        /// Gets dish by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Wanted dish.</returns>
        Task<DishRM> GetDish(Guid id);

        /// <summary>
        /// Gets all dishes of restaurant.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="restaurantId"></param>
        /// <param name="category"></param>
        /// <returns>Collection of restaurant's dishes.</returns>
        Task<List<DishRM>> GetAll(Guid? userId, Guid? restaurantId, int category);
    }
}