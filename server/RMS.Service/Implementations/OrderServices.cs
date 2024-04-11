using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;
using RMS.Data;
using RMS.Data.Models;
using RMS.Service.Contracts;
using RMS.Service.Identity.Services;
using RMS.Shared;
using RMS.Shared.Contracts;
using RMS.Shared.Enum;

namespace RMS.Service.Implementations
{
    public class OrderServices : IOrderServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<List<OrderRM>> MapOrder(List<Order> orders)
        {
            List<OrderRM> ordersRM = orders.Adapt<List<OrderRM>>();
            for (int i = 0; i < orders.Count; i++)
            {
                ordersRM[i].Restaurant = orders[i].Restaurant.Adapt<RestaurantRM>();
                ordersRM[i].UserFullName = orders[i].User?.FirstName + " " + orders[i].User?.LastName;
                ordersRM[i].Address = orders[i].User?.Address?.City + orders[i].User?.Address?.Street + " " + orders[i].User?.Address?.Number;
            }

            return ordersRM;
        }

        public async Task<OrderRM> GetOrder(Guid orderId, Guid? userId)
        {
            var order = await this._context.Orders.Include(o => o.Restaurant).Include(o => o.User).Where(o => o.Id == orderId).FirstOrDefaultAsync();

            if (order is null)
            {
                throw new Exception(ExceptionMessages.OrderMessage);
            }

            var user = await this._userManager.FindByIdAsync(userId.ToString());
            var roles = await this._userManager.GetRolesAsync(user);

            if (roles.Contains(Roles.User))
            {
                if (order.UserId != userId)
                {
                    throw new Exception(ExceptionMessages.UserIdOrderMessage);
                }
            }
            else
            {
                if (order.RestaurantId != user.RestaurantId)
                {
                    throw new Exception(ExceptionMessages.UserIdOrderMessage);
                }
            }

            var orderRM = order.Adapt<OrderRM>();
            orderRM.UserFullName = order?.User?.FirstName + " " + order?.User?.LastName;
            orderRM.Address = order?.User?.Address?.City + order?.User?.Address?.Street + " " + order?.User?.Address?.Number;
            return orderRM;
        }

        public async Task<List<OrderRM>> GetOrdersByUserId(Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var orders = await this._context.Orders.Include(o => o.Restaurant).Include(o => o.User).Where(o => o.UserId == userId).ToListAsync();

            if (orders is null)
            {
                throw new Exception(ExceptionMessages.OrderMessage);
            }

            var ordersVM = await this.MapOrder(orders);

            return ordersVM;
        }

        public async Task<List<OrderRM>> GetOrdersByRestaurantId(Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            if (user.RestaurantId is null)
            {
                throw new Exception("User doesn't have this permissions");
            }

            var ordersQ = this._context.Orders
                .Where(o => o.RestaurantId == user.RestaurantId && o.Status != OrderStatus.NotFinished && o.Status != OrderStatus.Canceled);
            List<Order> orders = new List<Order>();
            var roles = await this._userManager.GetRolesAsync(user);
            if (roles.Contains(Roles.Deliver))
            {
                orders = await ordersQ.Include(o => o.Restaurant).Include(o => o.User).Where(o => o.Status == OrderStatus.WaitCollection || o.Status == OrderStatus.Accepted).ToListAsync();
            }
            else
            {
                orders = await ordersQ.Include(o => o.Restaurant).Include(o => o.User).Where(o => o.Status >= OrderStatus.Finished).ToListAsync();
            }

            return await this.MapOrder(orders);
        }

        public async Task<List<OrderRM>> GetOrdersByUserRestaurantId(Guid restaurantId, Guid? userId)
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

            var orders = await this._context.Orders.Include(o => o.Restaurant).Include(o => o.User).Where(b => b.RestaurantId == restaurantId && b.UserId == userId).ToListAsync();

            if (orders is null)
            {
                throw new Exception(ExceptionMessages.OrderMessage);
            }

            return await this.MapOrder(orders);
        }

        public async Task<List<DishCountRM>> GetOrderDishes(Guid orderId, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            OrderRM order = new OrderRM();

            var roles = await this._userManager.GetRolesAsync(user);
            if (roles.Contains(Roles.User))
            {
                var userOrders = await this._context.Orders.Where(o => o.Id == orderId && o.UserId == userId).FirstOrDefaultAsync();
                order = userOrders.Adapt<OrderRM>();
            }

            if (roles.Contains(Roles.Manager) || roles.Contains(Roles.Employee) || roles.Contains(Roles.Deliver))
            {
                if (user.RestaurantId is null)
                {
                    throw new Exception();
                }

                var restaurantOrders = await this._context.Orders.Where(b => b.RestaurantId == user.RestaurantId).FirstOrDefaultAsync();
                order = restaurantOrders.Adapt<OrderRM>();
            }

            var orderDishes = new List<DishCountRM>();
            var orderDishesList = await this._context.OrdersDishes.Where(od => od.OrderId == orderId).ToListAsync();
            foreach (var orderDish in orderDishesList)
            {
                var dish = await this._context.Dishes.FindAsync(orderDish.DishId);
                DishCountRM dishCount = new DishCountRM();
                dishCount.Dish = dish.Adapt<DishRM>();
                dishCount.Count = orderDish.DishCount;
                orderDishes.Add(dishCount);
            }

            return orderDishes;
        }

        public async Task<bool> AssignDish(Guid dishId, int count, Guid? userId)
        {
            var dish = await this._context.Dishes.FindAsync(dishId);
            if (dish is null)
            {
                throw new Exception(ExceptionMessages.DishMessage);
            }

            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var order = this._context.Orders.Where(o => o.UserId == userId && o.RestaurantId == dish.RestaurantId && o.Status == OrderStatus.NotFinished).FirstOrDefault();

            OrdersDishes ordersDishes = new OrdersDishes();
            if (order is null)
            {
                var orderId = await this.Add(userId, dish.RestaurantId);
                if (orderId == Guid.Empty)
                {
                    return false;
                }

                ordersDishes = new OrdersDishes { DishCount = count, DishId = dishId, OrderId = orderId };
            }
            else
            {
                var wantedOrderDish = await this._context.OrdersDishes.Where(od => od.OrderId == order.Id && od.DishId == dishId).FirstOrDefaultAsync();
                if (wantedOrderDish is null)
                {
                    ordersDishes = new OrdersDishes { DishCount = count, DishId = dishId, OrderId = order.Id };
                }
                else
                {
                    await this.UpdateCount(order.Id, dish.Id, wantedOrderDish.DishCount + count, userId);
                }
            }

            await this._context.OrdersDishes.AddAsync(ordersDishes);
            await this._context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveDish(Guid orderId, Guid dishId, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var order = await this._context.Orders.FindAsync(orderId);
            if (order is null)
            {
                throw new Exception(ExceptionMessages.OrderMessage);
            }

            var dish = await this._context.Dishes.FindAsync(dishId);
            if (dish is null)
            {
                throw new Exception(ExceptionMessages.DishMessage);
            }

            var wantedOrderDish = await this._context.OrdersDishes
                .Where(od => od.OrderId == orderId && od.DishId == dishId)
                .FirstOrDefaultAsync();

            if (wantedOrderDish is null)
            {
                return false;
            }

            this._context.OrdersDishes.Remove(wantedOrderDish);
            await this._context.SaveChangesAsync();

            var forRemoveOrder = await this._context.OrdersDishes.Where(od => od.OrderId == order.Id).ToListAsync();
            if (forRemoveOrder.Count == 0)
            {
                this._context.Orders.Remove(order);
                await this._context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UpdateCount(Guid orderId, Guid dishId, int? count, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var order = await this._context.Orders.FindAsync(orderId);
            if (order is null)
            {
                throw new Exception(ExceptionMessages.OrderMessage);
            }

            if (order.UserId != userId)
            {
                return false;
            }

            var dish = await this._context.Dishes.FindAsync(dishId);
            if (dish is null)
            {
                throw new Exception();
            }

            var wantedOrderDish = await this._context.OrdersDishes
                .Where(od => od.OrderId == orderId && od.DishId == dishId)
                .FirstOrDefaultAsync();

            if (wantedOrderDish is null)
            {
                throw new Exception(ExceptionMessages.WantedEntity);
            }

            if (count > 0)
            {
                wantedOrderDish.DishCount = count;
                this._context.OrdersDishes.Update(wantedOrderDish);
            }
            else
            {
                this._context.OrdersDishes.Remove(wantedOrderDish);
            }

            await this._context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(OrderIM orderIM, Guid orderId, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var order = await this._context.Orders.FindAsync(orderId);
            if (order is null)
            {
                throw new Exception(ExceptionMessages.OrderMessage);
            }

            if (order.Status != OrderStatus.Delivered)
            {
                var roles = await this._userManager.GetRolesAsync(user);

                if (roles.Contains(Roles.User))
                {
                    if (order.UserId != userId)
                    {
                        throw new Exception(ExceptionMessages.UserIdOrderMessage);
                    }
                }
                else
                {
                    if (order.RestaurantId != user.RestaurantId)
                    {
                        throw new Exception(ExceptionMessages.RestaurantIdOrderMassage);
                    }
                }

                order.Status = orderIM.Status;
                if (orderIM.Date == default!)
                {
                    order.Date = DateTime.Now;
                }
                else
                {
                    if (orderIM.DeliveryDate != default!)
                    {
                        order.DeliveryDate = orderIM.DeliveryDate;
                    }
                }

                if (order.Status == OrderStatus.Accepted)
                {
                    order.DeliveryId = user.Id;
                }

                if (orderIM.Status == OrderStatus.Delivered)
                {
                    order.DeliveryDate = DateTime.Now;
                }

                await this._context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Guid> Add(Guid? userId, Guid? restaurantId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            Order order = new Order();
            order.UserId = userId;
            order.Status = OrderStatus.NotFinished;
            order.RestaurantId = restaurantId;

            await this._context.Orders.AddAsync(order);
            await this._context.SaveChangesAsync();

            return order.Id;
        }
    }
}
