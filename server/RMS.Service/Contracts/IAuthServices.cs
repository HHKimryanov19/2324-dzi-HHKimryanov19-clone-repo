using RMS.Shared.Models.InputModels;
using RMS.Shared.Models;
using System.Security.Claims;

namespace RMS.Service.Contracts
{
    public interface IAuthServices
    {
        /// <summary>
        /// Checks email and password and creates new jwt token.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns>New Jwt.</returns>
        Task<string> Login(LoginModel loginModel);

        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <param name="userIM"></param>
        /// <returns>True if everything about restration is alright, otherwise false.</returns>
        Task<bool> Register(UserIM userIM);
    }
}
