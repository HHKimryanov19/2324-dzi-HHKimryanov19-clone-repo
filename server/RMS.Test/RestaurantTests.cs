using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Service.Implementations;
using Moq;
using Microsoft.AspNetCore.Identity;
using RMS.Data.Models;
using RMS.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OBS.Shared.Models.ResponseModels;
using OBS.Shared.Models.InputModels;

namespace RMS.Test
{
    public class RestaurantTests
    {
        private ApplicationDbContext _context;
        private RestaurantServices _services;
        private Guid mockGuid;
        private Mock<UserManager<ApplicationUser>> _userManager;
        private Guid _restaurantId;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestRMSDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            mockGuid = new Guid("44015C4F-2385-44DD-E910-08DC502D1B06");

            //var userStoreMock = new Mock<IUserStore<ApplicationUser>>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _services = new RestaurantServices(_context, mockUserManager.Object);

            var user = new ApplicationUser()
            {
                FirstName = "TestFirstName",
                LastName = "TestFirstName",
                Address = new Address
                {
                    Country = "Country",
                    City = "City",
                    Street = "Street",
                    Number = "Number"
                },
                Email = "test@gmail.com",
            };

            mockUserManager.Setup(m => m.FindByIdAsync(mockGuid.ToString())).ReturnsAsync(user);
            mockUserManager.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });

            _userManager = mockUserManager;
        }

        [Test]
        public async Task AddRestaurant_ShouldReturnRestaurantId()
        {
            var result = "Pizza Restaurant";

            RestaurantIM restaurantIM = new RestaurantIM
            {
                Name = "Pizza Restaurant",
                Phone = "089999999999",
                DeliveryPrice = 1.4M,
                Address = new Address
                {
                    Country = "Country",
                    City = "Burgas",
                    Street = "Street",
                    Number = "Number"
                },
                Image = null
            };

            var restaurantId = await _services.Add(restaurantIM);

            RestaurantRM restaurant = new RestaurantRM();
            restaurant = await _services.GetRestaurant(restaurantId);

            Assert.That(restaurant.Name, Is.EqualTo(result));
        }

        [Test]
        public async Task GetRestaurants_ShouldReturnRestaurantsInCity()
        {
            int count = 2;

            RestaurantIM newRestaurantOne = new RestaurantIM
            {
                Name = "RestaurantOne",
                Phone = "089999999999",
                DeliveryPrice = 1.4M,
                Address = new Address
                {
                    Country = "Country",
                    City = "City",
                    Street = "Street",
                    Number = "Number"
                },
                Image = null
            };

            RestaurantIM newRestaurantTwo = new RestaurantIM
            {
                Name = "RestaurantTwo",
                Phone = "089999999999",
                DeliveryPrice = 1.4M,
                Address = new Address
                {
                    Country = "Country",
                    City = "City",
                    Street = "Street",
                    Number = "Number"
                },
                Image = null
            };

            await _services.Add(newRestaurantOne);
            await _services.Add(newRestaurantTwo);

            List<RestaurantRM> restaurantVMs = new List<RestaurantRM>();
            restaurantVMs = await _services.GetRestaurants(mockGuid);

            Assert.That(restaurantVMs.Count, Is.EqualTo(count));
        }

        [Test]
        public async Task Delete_ShouldReturnTrue()
        {
            bool expected = true;
            
            RestaurantIM newRestaurant = new RestaurantIM
            {
                Name = "RestaurantThree",
                Phone = "089999999999",
                DeliveryPrice = 1.4M,
                Address = new Address
                {
                    Country = "Country",
                    City = "Varna",
                    Street = "Street",
                    Number = "Number"
                },
                Image = null
            };

            var restaurantId = await _services.Add(newRestaurant);
            bool result = await _services.Delete(restaurantId, mockGuid);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}