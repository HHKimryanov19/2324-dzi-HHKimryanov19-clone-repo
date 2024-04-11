using RMS.Shared.Enum;

namespace RMS.Data.Models
{
    /// <summary>
    /// Class representing Booking entity in database.
    /// </summary>
    public class Booking : BaseEntity
    {
        public int NumberOfPeople { get; set; }

        public DateTime? Date { get; set; }

        public bool IsInside { get; set; }

        public Guid? RestaurantId { get; set; }

        public Restaurant? Restaurant { get; set; }

        public Guid? UserId { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
