using RMS.Shared.Models;
using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;

namespace RMS.Service.Contracts
{
    public interface IEmployeeServices
    {
        /// <summary>
        /// Adds employee or deliver using email.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="isNormalEmployee"></param>
        /// <param name="userId"></param>
        /// <returns>Employee or deliver's Id.</returns>
        Task<Guid> AddEmployee(string email, bool isNormalEmployee, Guid? userId);

        /// <summary>
        /// Adds manager using email.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="restaurantId"></param>
        /// <returns>Manager's Id.</returns>
        Task<Guid?> AddManager(string email, Guid restaurantId);
    }
}
