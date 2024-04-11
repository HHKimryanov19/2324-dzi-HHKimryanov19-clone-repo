using RMS.Shared.Enum;

namespace RMS.Shared.Models.ResponseModels
{
    /// <summary>
    /// Represents a model for response of Order entity.
    /// </summary>
    public class OrderRM : BaseRMEntity
    {
        public OrderStatus? Status { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public Guid? RestaurantId { get; set; }

        public RestaurantRM? Restaurant { get; set; }

        public string? UserFullName { get; set; }

        public string? Address { get; set; }
    }
}
