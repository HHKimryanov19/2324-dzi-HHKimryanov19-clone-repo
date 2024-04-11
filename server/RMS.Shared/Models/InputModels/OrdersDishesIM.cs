namespace RMS.Shared.Models.InputModels
{
    /// <summary>
    /// Represents a model for adding or updating OrdersDishes entity.
    /// </summary>
    public class OrdersDishesIM
    {
        public Guid OrderId { get; set; }

        public Guid DishId { get; set; }

        public int Count { get; set; }
    }
}
