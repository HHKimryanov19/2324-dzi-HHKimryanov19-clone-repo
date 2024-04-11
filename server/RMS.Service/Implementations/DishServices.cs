using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;
using RMS.Data;
using RMS.Data.Models;
using RMS.Service.Contracts;
using RMS.Service.Identity.Services;
using RMS.Shared.Contracts;
using RMS.Shared.Enum;
using RMS.Shared;

namespace RMS.Service.Implementations
{
    public class DishServices : IDishServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DishServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<Guid> Add(DishIM dish, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            if (dish is null)
            {
                throw new Exception(ExceptionMessages.DishMessage);
            }

            if (user.RestaurantId is not null)
            {
                Dish newDish = dish.Adapt<Dish>();
                newDish.RestaurantId = user.RestaurantId;

                if (dish.Image is not null)
                {
                    var imageStream = new MemoryStream();
                    dish.Image.CopyTo(imageStream);
                    newDish.ImageBytes = imageStream.ToArray();
                }

                await this._context.Dishes.AddAsync(newDish);
                await this._context.SaveChangesAsync();
                return newDish.Id;
            }
            else
            {
                return Guid.NewGuid();
            }
        }

        public async Task<bool> Delete(Guid dishId, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var dish = await this._context.Dishes.FindAsync(dishId);

            if (dish is null)
            {
                throw new Exception(ExceptionMessages.DishMessage);
            }

            if (dish.RestaurantId != user.RestaurantId)
            {
                return false;
            }

            this._context.Dishes.Remove(dish);
            await this._context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DishRM>> GetAll(Guid? userId, Guid? restaurantId, int category)
        {
            List<DishRM> dishes = new List<DishRM>();

            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            IQueryable<Dish> dishesQ;
            if (user.RestaurantId is null)
            {
                if (category == 0)
                {
                    dishesQ = this._context.Dishes.Where(d => d.RestaurantId == restaurantId);
                }
                else
                {
                    dishesQ = this._context.Dishes.Where(d => d.Category == (FoodCategory)category && d.RestaurantId == restaurantId);
                }
            }
            else
            {
                restaurantId = user.RestaurantId;
                dishesQ = this._context.Dishes.Where(d => d.RestaurantId == restaurantId);
            }

            dishes = dishesQ.ToList().Adapt<List<DishRM>>();

            return dishes;
        }

        public async Task<DishRM> GetDish(Guid id)
        {
            var dish = await this._context.Dishes.FindAsync(id);

            if (dish is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            DishRM dishVM = dish.Adapt<DishRM>();
            return dishVM;
        }

        public async Task<bool> Update(DishIM dishIM, Guid dishId, Guid? userId)
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

            if (dish.RestaurantId != user.RestaurantId)
            {
                return false;
            }

            dish.Title = dishIM.Title;
            dish.Info = dishIM.Info;
            dish.Price = dishIM.Price;

            if (dishIM.Image is not null)
            {
                var imageStream = new MemoryStream();
                dishIM.Image.CopyTo(imageStream);
                dish.ImageBytes = imageStream.ToArray();
            }

            dish.Category = dishIM.Category;

            this._context.Dishes.Update(dish);
            await this._context.SaveChangesAsync();
            return true;
        }
    }
}
