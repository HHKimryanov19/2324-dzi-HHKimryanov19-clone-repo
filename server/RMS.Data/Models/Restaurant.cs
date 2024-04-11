using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Shared.Models;

namespace RMS.Data.Models
{
    /// <summary>
    /// Class representing Restaurant entity in database.
    /// </summary>
    public class Restaurant : BaseEntity
    {
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public byte[] ImageBytes { get; set; } = default!;

        public Address? Address { get; set; } = default!;

        public decimal? DeliveryPrice { get; set; }

        public ICollection<Booking>? Bookings { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Dish>? Dishes { get; set; }

        public ICollection<ApplicationUser>? Employees { get; set; }
    }
}
