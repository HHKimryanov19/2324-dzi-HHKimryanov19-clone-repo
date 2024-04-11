using Microsoft.AspNetCore.Http;
using RMS.Shared.Enum;

namespace RMS.Shared.Models.ResponseModels
{
    /// <summary>
    /// Represents a model for response of Dish entity.
    /// </summary>
    public class DishRM : BaseRMEntity
    {
        public string? Title { get; set; }

        public string? Info { get; set; }

        public decimal Price { get; set; }

        public byte[] ImageBytes { get; set; }

        public FoodCategory Category { get; set; }
    }
}
