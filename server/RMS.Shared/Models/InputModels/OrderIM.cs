using RMS.Shared.Enum;

namespace RMS.Shared.Models.InputModels
{
    /// <summary>
    /// Represents a model for adding or updating Order entity.
    /// </summary>
    public class OrderIM
    {
        public OrderStatus? Status { get; set; }

        public DateTime? Date { get; set; } = default!;

        public DateTime? DeliveryDate { get; set; }
    }
}