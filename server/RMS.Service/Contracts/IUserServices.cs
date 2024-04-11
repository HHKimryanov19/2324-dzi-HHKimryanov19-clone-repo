using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;
using RMS.Shared.Models;

namespace RMS.Service.Contracts
{
    public interface IUserServices
    {
        /// <summary>
        /// Deletes user account using it email.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about deleting is alright, otherwise false.</returns>
        Task<bool> Delete(string email, Guid? userId);

        /// <summary>
        /// Update user information.
        /// </summary>
        /// <param name="newInfo"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about updating is alright, otherwise false.</returns>
        Task<bool> Update(UserIM newInfo, Guid? userId);

        /// <summary>
        /// Changes user password entering new and old.
        /// </summary>
        /// <param name="passwords"></param>
        /// <param name="userId"></param>
        /// <returns>True if everything about changing password is alright, otherwise false.</returns>
        Task<bool> ChangePassword(PasswordsModel passwords, Guid? userId);

        /// <summary>
        /// Gets user information.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User information.</returns>
        Task<UserRM> GetUser(Guid? userId);
    }
}
