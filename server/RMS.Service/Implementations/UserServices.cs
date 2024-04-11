using Mapster;
using Microsoft.AspNetCore.Identity;
using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;
using RMS.Data;
using RMS.Data.Models;
using RMS.Service.Contracts;
using RMS.Shared.Models;
using RMS.Shared;

namespace RMS.Service.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<UserRM> GetUser(Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            return user.Adapt<UserRM>();
        }

        public async Task<bool> ChangePassword(PasswordsModel passwords, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            var result = await this._userManager.ChangePasswordAsync(user, passwords.OldPassword, passwords.NewPassword);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(string email, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            if (email == user.Email)
            {
                var result = await this._userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return false;
                }
            }

            this._context.SaveChanges();
            return true;
        }

        public async Task<bool> Update(UserIM newInfo, Guid? userId)
        {
            var user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user is null || newInfo is null)
            {
                throw new Exception(ExceptionMessages.UserMessage);
            }

            user.FirstName = newInfo.FirstName;
            user.LastName = newInfo.LastName;
            user.Address = newInfo.Address;

            var result = await this._userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
