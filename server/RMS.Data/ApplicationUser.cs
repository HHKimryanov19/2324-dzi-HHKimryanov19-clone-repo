using Microsoft.AspNetCore.Identity;
using RMS.Data.Models;
using RMS.Shared.Models;

namespace RMS.Data;

/// <summary>
/// Cless representing AspNetUsers table in database.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public Address? Address { get; set; } = default!;

    public ICollection<Booking>? Bookings { get; set; } = default!;

    public ICollection<Order>? Orders { get; set; } = default!;

    public Guid? RestaurantId { get; set; }

    public Restaurant? Restaurant { get; set; }
}
