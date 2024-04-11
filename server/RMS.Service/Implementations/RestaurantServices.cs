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
using System.Runtime.InteropServices;

namespace RMS.Service.Implementations
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RestaurantServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<Guid> Add(RestaurantIM restaurant)
        {
            if (restaurant is null)
            {
                throw new Exception(ExceptionMessages.RestaurantMassage);
            }

            var newEntity = restaurant.Adapt<Restaurant>();
            if (restaurant.Image is not null)
            {
                var imageStream = new MemoryStream();
                restaurant.Image.CopyTo(imageStream);
                newEntity.ImageBytes = imageStream.ToArray();
            }

            await this._context.Restaurants.AddAsync(newEntity);
            await this._context.SaveChangesAsync();

            return newEntity.Id;
        }

        public async Task<bool> Delete(Guid restaurantId, Guid? userId)
        {
            var admin = await this._userManager.FindByIdAsync(userId.ToString());
            if (admin is null)
            {
                return false;
            }

            Restaurant restaurant = await this._context.Restaurants.FindAsync(restaurantId);

            if (restaurant is null)
            {
                return false;
            }

            this._context.Restaurants.Remove(restaurant);
            await this._context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RestaurantRM>> GetRestaurants(Guid? userId)
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            var user = await this._userManager.FindByIdAsync(userId.ToString());
            var roles = await this._userManager.GetRolesAsync(user);

            if (roles is not null)
            {
                if (roles.Contains(Roles.Admin))
                {
                    restaurants = await this._context.Restaurants.ToListAsync();
                }
                else
                {
                    if (user.Address.City == "")
                    {
                        restaurants = await this._context.Restaurants.ToListAsync();
                    }
                    else
                    {
                        restaurants = await this._context.Restaurants.Where(r => r.Address.City == user.Address.City).ToListAsync();
                    }
                }
            }

            List<RestaurantRM> restauransVM = restaurants.Adapt<List<RestaurantRM>>();

            for (int i = 0; i < restaurants.Count; i++)
            {
                if (restaurants[i].ImageBytes is not null)
                {
                    restauransVM[i].Image = Convert.ToBase64String(restaurants[i].ImageBytes);
                }
            }

            return restauransVM;
        }

        public async Task<RestaurantRM> GetRestaurant(Guid? id)
        {
            Restaurant restaurant = await this._context.Restaurants.FindAsync(id);

            if (restaurant is null)
            {
                throw new Exception(ExceptionMessages.RestaurantMassage);
            }

            RestaurantRM restaurantVM = restaurant.Adapt<RestaurantRM>();
            if (restaurant.ImageBytes is not null)
            {
                restaurantVM.Image = Convert.ToBase64String(restaurant.ImageBytes);
            }

            return restaurantVM;
        }

        public async Task<bool> Update(RestaurantIM restaurantIM, Guid restaurantId, Guid? userId)
        {
            if (restaurantIM is null)
            {
                throw new Exception(ExceptionMessages.RestaurantMassage);
            }

            var user = await this._userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var roles = await this._userManager.GetRolesAsync(user);
            var restaurant = await this._context.Restaurants.FindAsync(restaurantId);

            if (roles.Contains(Roles.Employee) || roles.Contains(Roles.Manager))
            {
                if (user.RestaurantId is null)
                {
                    throw new Exception();
                }

                var restaurantIdE = user.RestaurantId;

                if (restaurantId == restaurantIdE)
                {
                    restaurant.Name = restaurantIM.Name;
                    restaurant.Address = restaurantIM?.Address;
                    restaurant.Phone = restaurantIM?.Name;
                    restaurant.DeliveryPrice = restaurantIM.DeliveryPrice;

                    this._context.Restaurants.Update(restaurant);
                    await this._context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (roles.Contains(Roles.Admin))
            {
                restaurant.Name = restaurantIM?.Name;
                restaurant.Address = restaurantIM?.Address;
                restaurant.Phone = restaurantIM?.Phone;
                restaurant.DeliveryPrice = restaurantIM.DeliveryPrice;

                if (restaurantIM.Image is not null)
                {
                    var imageStream = new MemoryStream();
                    restaurantIM.Image.CopyTo(imageStream);
                    restaurant.ImageBytes = imageStream.ToArray();
                }

                this._context.Restaurants.Update(restaurant);
                await this._context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
