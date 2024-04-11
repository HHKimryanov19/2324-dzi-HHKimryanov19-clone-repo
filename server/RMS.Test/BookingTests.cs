using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using RMS.Shared.Models.InputModels;
using RMS.Data;
using RMS.Data.Models;
using RMS.Service.Contracts;
using RMS.Service.Implementations;
using RMS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RMS.Test
{
    public class BookingTests
    {
        private ApplicationDbContext _context;
        private RestaurantServices _restaurantService;
        private BookingServices _bookingServices;
        private Guid mockGuid;
        private Mock<UserManager<ApplicationUser>> _userManager;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestRMSDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            mockGuid = new Guid("44015C4F-2385-44DD-E910-08DC502D1B06");

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _restaurantService = new RestaurantServices(_context, mockUserManager.Object);

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

            _bookingServices = new BookingServices(_context, mockUserManager.Object);

            _userManager = mockUserManager;
        }

        [Test]
        public async Task AddBooking_ShouldReturnNewBookingId()
        {
            RestaurantIM newRestaurant = new RestaurantIM
            {
                Name = "Pizza Restaurant",
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

            var restaurantId = await _restaurantService.Add(newRestaurant);

            BookingIM newBooking = new BookingIM 
            { 
                NumberOfPeople = 3, 
                Date = new DateTime(), 
                IsInside = false 
            };

            var bookingId = await _bookingServices.Add(newBooking, restaurantId, mockGuid);
            var booking = await _bookingServices.GetBooking(bookingId, mockGuid);

            Assert.That(booking, !Is.EqualTo(null));
        }

        [Test]
        public async Task Delete_ShouldReturnTrue()
        {
            RestaurantIM newRestaurant = new RestaurantIM
            {
                Name = "Pizza Restaurant",
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

            var restaurantId = await _restaurantService.Add(newRestaurant);

            BookingIM newBooking = new BookingIM
            {
                NumberOfPeople = 3,
                Date = new DateTime(),
                IsInside = false
            };

            var bookingId = await _bookingServices.Add(newBooking, restaurantId, mockGuid);
            var result = await _bookingServices.Delete(bookingId, mockGuid);

            if (result)
            {
                try
                {
                    var booking = await _bookingServices.GetBooking(bookingId, mockGuid);
                    Assert.IsTrue(false);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Booking doesn't exist or something get wrong")
                    {
                        Assert.IsTrue(true);
                    }
                }
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [Test]
        public async Task GetAllByUserId_ShouldReturnAllUserBookings()
        {
            int count = 4;
            RestaurantIM newRestaurant = new RestaurantIM
            {
                Name = "Pizza Restaurant",
                Phone = "089999999999",
                DeliveryPrice = 1.4M,
                Address = new Address
                {
                    Country = "Country",
                    City = "Sofia",
                    Street = "Street",
                    Number = "Number"
                },
                Image = null
            };

            var restaurantId = await _restaurantService.Add(newRestaurant);

            BookingIM newBooking = new BookingIM
            {
                NumberOfPeople = 3,
                Date = new DateTime(),
                IsInside = false
            };

            await _bookingServices.Add(newBooking, restaurantId, mockGuid);
            await _bookingServices.Add(newBooking, restaurantId, mockGuid);
            await _bookingServices.Add(newBooking, restaurantId, mockGuid);

            var bookings = await _bookingServices.GetAllByUserId(mockGuid);

            Assert.That(bookings.Count, Is.EqualTo(count));
        }
    }
}
