using RMS.Shared.Enum;
using RMS.Shared.Models;

namespace RMS.Data.Models
{
    /// <summary>
    /// Class representing Order entity in database.
    /// </summary>
    public class Order : BaseEntity
    {
        public Order()
        {
            this.OrdersDishes = new HashSet<OrdersDishes>();
        }

        public DateTime? Date { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public OrderStatus? Status { get; set; }

        public Guid? RestaurantId { get; set; }

        public Restaurant? Restaurant { get; set; }

        public Guid? UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public Guid? DeliveryId { get; set; }

        public ApplicationUser? Delivery { get; set; }

        public ICollection<OrdersDishes>? OrdersDishes { get; }
    }
}
