using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RMS.Shared.Models.InputModels;
using RMS.Data;
using RMS.Data.Models;
using RMS.Service.Contracts;
using RMS.Shared;
using RMS.Shared.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RMS.Service.Implementations
{
    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration config)
        {
            this._context = context;
            this._config = config;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        /// <summary>
        /// Creates Jwt.
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns>New Jwt.</returns>
        private async Task<string> CreateToken(ICollection<Claim> authClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var sectoken = new JwtSecurityToken(null, expires: DateTime.Now.AddHours(2), signingCredentials: credentials, claims: authClaims);

            var token = new JwtSecurityTokenHandler().WriteToken(sectoken);

            return token;
        }

        /// <summary>
        /// Seeds admin account.
        /// </summary>
        /// <returns>True if everything about seeding admin is alright, otherwise false.</returns>
        public async Task<bool> AdminSeed()
        {
            Address address = new Address();
            address.City = "City";
            address.Country = "Country";
            address.Street = "Street";
            address.Number = "Number";

            var admin = new
            {
                UserName = "AdminUser",
                Password = "Password",
                Email = "adminRMS@codingburgas.bg",
                FirstName = "AdminFirstName",
                LastName = "AdminLastName",
                Address = address,
            };

            var userExists = await this._userManager.FindByEmailAsync(admin.Email);
            if (userExists != null)
            {
                return false;
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = admin.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = admin.UserName,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
            };

            var result = await this._userManager.CreateAsync(user, admin.Password);
            if (!result.Succeeded)
            {
                return false;
            }

            var roleResult = await this._userManager.AddToRoleAsync(user, Roles.Admin);
            if (!roleResult.Succeeded)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Seeds all roles.
        /// </summary>
        /// <returns>True if everything about seeding roles is alright, otherwise false.</returns>
        public async Task<bool> SeedRoles()
        {
            if (!await this._roleManager.RoleExistsAsync(Roles.User))
            {
                await this._roleManager.CreateAsync(new IdentityRole<Guid>(Roles.User));
            }

            if (!await this._roleManager.RoleExistsAsync(Roles.Employee))
            {
                await this._roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Employee));
            }

            if (!await this._roleManager.RoleExistsAsync(Roles.Manager))
            {
                await this._roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Manager));
            }

            if (!await this._roleManager.RoleExistsAsync(Roles.Admin))
            {
                await this._roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Admin));
            }

            if (!await this._roleManager.RoleExistsAsync(Roles.Deliver))
            {
                await this._roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Deliver));
            }

            return true;
        }

        /// <inheritdoc />
        public async Task<string> Login(LoginModel loginModel)
        {
            var user = await this._userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
            }

            var isPasswordCorrect = await this._userManager.CheckPasswordAsync(user, loginModel.Password);

            if (!isPasswordCorrect)
            {
                throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
            }

            var userRoles = await this._userManager.GetRolesAsync(user);

            var authClaims = await this._userManager.GetClaimsAsync(user);
            authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            string token = await CreateToken(authClaims);

            return token;
        }

        /// <inheritdoc />
        public async Task<bool> Register(UserIM userIM)
        {
            var userExists = await this._userManager.FindByEmailAsync(userIM.Email);
            if (userExists != null)
            {
                throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = userIM.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userIM.Email.Remove(userIM.Email.IndexOf('@')),
                FirstName = userIM.FirstName,
                LastName = userIM.LastName,
                Address = userIM.Address,
            };

            var result = await this._userManager.CreateAsync(user, userIM.Password);
            if (!result.Succeeded)
            {
                return false;
            }

            var roleResult = await this._userManager.AddToRoleAsync(user, Roles.User);
            if (!roleResult.Succeeded)
            {
                throw new Exception(ExceptionMessages.InvalidCreditinalsMessage);
            }

            return true;
        }
    }
}