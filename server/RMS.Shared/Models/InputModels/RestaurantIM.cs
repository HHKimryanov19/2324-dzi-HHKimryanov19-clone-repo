using Microsoft.AspNetCore.Http;
using RMS.Shared.Models;

namespace RMS.Shared.Models.InputModels
{
    /// <summary>
    /// Represents a model for adding or updating Restaurant entity.
    /// </summary>
    public class RestaurantIM
    {
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public decimal DeliveryPrice { get; set; } = 0;

        public Address Address { get; set; } = default!;

        public IFormFile? Image { get; set; }
    }
}
