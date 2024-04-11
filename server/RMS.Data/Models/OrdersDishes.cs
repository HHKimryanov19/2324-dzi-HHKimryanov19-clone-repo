using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Data.Models
{
    /// <summary>
    /// Class representing OrdersDishes entity in database.
    /// </summary>
    public class OrdersDishes : BaseEntity
    {
        public Guid? OrderId { get; set; }

        public Guid? DishId { get; set; }

        public Order? Order { get; set; }

        public Dish? Dish { get; set; }

        public int? DishCount { get; set; }
    }
}
