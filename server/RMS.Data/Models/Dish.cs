using RMS.Shared.Enum;

namespace RMS.Data.Models
{
    /// <summary>
    /// Class representing Dish entity in database.
    /// </summary>
    public class Dish : AuditableEntity
    {
        public Dish()
        {
            this.OrdersDishes = new HashSet<OrdersDishes>();
        }

        public string? Title { get; set; }

        public string? Info { get; set; }

        public decimal? Price { get; set; }

        public byte[] ImageBytes { get; set; } = default!;

        public FoodCategory? Category { get; set; }

        public Guid? RestaurantId { get; set; }

        public Restaurant? Restaurant { get; set; }

        public ICollection<OrdersDishes>? OrdersDishes { get; }
    }
}
