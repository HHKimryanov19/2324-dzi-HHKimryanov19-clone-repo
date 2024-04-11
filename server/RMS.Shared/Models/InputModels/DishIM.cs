using Microsoft.AspNetCore.Http;
using RMS.Shared.Enum;

namespace RMS.Shared.Models.InputModels
{
    /// <summary>
    /// Represents a model for adding or updating Dish entity.
    /// </summary>
    public class DishIM
    {
        public string? Title { get; set; }

        public string? Info { get; set; }

        public decimal Price { get; set; }

        public FoodCategory Category { get; set; }

        public IFormFile? Image { get; set; }
    }
}
