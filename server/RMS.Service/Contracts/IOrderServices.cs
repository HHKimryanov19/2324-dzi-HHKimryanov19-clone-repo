using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;

namespace RMS.Service.Contracts
{
    public interface IOrderServices
    {
        /// <summary>
        /// Gets order by Id.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns>Wanted order.</returns>
        Task<OrderRM> GetOrder(Guid orderId, Guid? userId);

        /// <summary>
        /// Gets all orders for a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Collection of user's orders.</returns>
        Task<List<OrderRM>> GetOrdersByUserId(Guid? userId);

        /// <summary>
        /// Gets orders for a restaurant.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Collection of orders for restaurant.</returns>
        Task<List<OrderRM>> GetOrdersByRestaurantId(Guid? userId);

        /// <summary>
        /// Gets all orders for a user in specific restaruant.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="userId"></param>
        /// <returns>Collection of user's order for given restaruant.</returns>
        Task<List<OrderRM>> GetOrdersByUserRestaurantId(Guid restaurantId, Guid? userId);

        /// <summary>
        /// Assigns dish to not finished order of this restaurant.
        /// </summary>
        /// <param name="dishId"></param>
        /// <param name="count"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about assign is alright, otherwise false.</returns>
        Task<bool> AssignDish(Guid dishId, int count, Guid? userId);

        /// <summary>
        /// Removes dish from order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="dishId"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about remove is alright, otherwise false.</returns>
        Task<bool> RemoveDish(Guid orderId, Guid dishId, Guid? userId);

        /// <summary>
        /// Updates count of dish in order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="dishId"></param>
        /// <param name="count"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about updating count is alright, otherwise false.</returns>
        Task<bool> UpdateCount(Guid orderId, Guid dishId, int? count, Guid? userId);

        /// <summary>
        /// Updates order information.
        /// </summary>
        /// <param name="orderIM"></param>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about updating is alright, otherwise false.</returns>
        Task<bool> Update(OrderIM orderIM, Guid orderId, Guid? userId);

        /// <summary>
        /// Get all dishes and their count in given order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns>Collection of dishes and their count in the order.</returns>
        Task<List<DishCountRM>> GetOrderDishes(Guid orderId, Guid? userId);
    }
}
