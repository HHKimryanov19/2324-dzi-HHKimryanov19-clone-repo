using RMS.Shared.Models;

namespace RMS.Shared.Models.ResponseModels
{
    /// <summary>
    /// Represents a model for response of Restaurant entity.
    /// </summary>
    public class RestaurantRM : BaseRMEntity
    {
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public decimal DeliveryPrice { get; set; }

        public Address Address { get; set; } = default!;

        public string? Image { get; set; }
    }
}
