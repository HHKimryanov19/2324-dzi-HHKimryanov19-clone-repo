namespace RMS.Shared.Models.InputModels
{
    /// <summary>
    /// Represents a model for adding or updating Booking entity.
    /// </summary>
    public class BookingIM
    {
        public int NumberOfPeople { get; set; }

        public DateTime Date { get; set; }

        public bool IsInside { get; set; }
    }
}
