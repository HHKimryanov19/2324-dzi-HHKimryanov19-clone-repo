namespace RMS.Shared.Models.ResponseModels
{
    /// <summary>
    /// Represents a model for response of Booking entity.
    /// </summary>
    public class BookingRM : BaseRMEntity
    {
        public int NumberOfPeople { get; set; }

        public DateTime Date { get; set; }

        public bool IsInside { get; set; }

        public Guid RestaurantId { get; set; }

        public string? FullUserName { get; set; }

        public string? RestaurantName { get; set; }
    }
}
