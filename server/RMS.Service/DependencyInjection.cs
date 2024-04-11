using Microsoft.Extensions.DependencyInjection;
using RMS.Service.Contracts;
using RMS.Service.Identity.Services;
using RMS.Service.Implementations;
using RMS.Shared.Contracts;

namespace RMS.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IBookingServices, BookingServices>();
            services.AddScoped<IDishServices, DishServices>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IRestaurantServices, RestaurantServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IAuthServices, AuthServices>();

            return services;
        }
    }
}