using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Data.Models;
using RMS.Service.Contracts;
using RMS.Service.Identity.Services;
using RMS.Shared;
using RMS.Shared.Contracts;
using RMS.Shared.Models;
using RMS.Shared.Models.InputModels;
using RMS.Shared.Models.ResponseModels;

namespace RMS.Service.Implementations;

public class EmployeeServices : IEmployeeServices
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmployeeServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext context)
    {
        this._context = context;
        this._userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<Guid> AddEmployee(string email, bool isNormalEmployee, Guid? userId)
    {
        var manager = await this._userManager.FindByIdAsync(userId.ToString());
        if (manager is null)
        {
            throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
        }

        var employee = await this._userManager.FindByEmailAsync(email);
        if (employee is null)
        {
            throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
        }

        if (employee.RestaurantId is not null || manager.RestaurantId is null)
        {
            throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
        }

        var roleResult = new IdentityResult();
        if (isNormalEmployee)
        {
            roleResult = await this._userManager.AddToRoleAsync(employee, Roles.Employee);
            if (!roleResult.Succeeded)
            {
                throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
            }
        }
        else
        {
            roleResult = await this._userManager.AddToRoleAsync(employee, Roles.Deliver);
            if (!roleResult.Succeeded)
            {
                throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
            }
        }

        roleResult = await this._userManager.RemoveFromRoleAsync(employee, Roles.User);
        if (!roleResult.Succeeded)
        {
            throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
        }

        employee.RestaurantId = manager.RestaurantId;

        this._context.Users.Update(employee);
        await this._context.SaveChangesAsync();

        return employee.Id;
    }

    /// <inheritdoc />
    public async Task<Guid?> AddManager(string email, Guid restaurantId)
    {
        var employee = await this._userManager.FindByEmailAsync(email);
        if (employee is null)
        {
            throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
        }

        var roles = await this._userManager.GetRolesAsync(employee);

        if (roles.Contains(Roles.User))
        {
            var roleResult = await this._userManager.RemoveFromRoleAsync(employee, Roles.User);
            if (!roleResult.Succeeded)
            {
                throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
            }
        }

        if (!roles.Contains(Roles.Manager))
        {
            var roleResult = await this._userManager.AddToRoleAsync(employee, Roles.Manager);
            if (!roleResult.Succeeded)
            {
                throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
            }
        }

        if (roles.Contains(Roles.Employee))
        {
            var roleResult = await this._userManager.RemoveFromRoleAsync(employee, Roles.Employee);
            if (!roleResult.Succeeded)
            {
                throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
            }
        }

        if (employee.RestaurantId is not null)
        {
            throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
        }

        employee.RestaurantId = restaurantId;

        this._context.Users.Update(employee);
        await this._context.SaveChangesAsync();

        return employee.Id;
    }
}
