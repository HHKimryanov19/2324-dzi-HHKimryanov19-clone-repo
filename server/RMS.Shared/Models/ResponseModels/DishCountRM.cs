using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Shared.Models.ResponseModels
{
    /// <summary>
    /// Represents a model for response of dish and its count in order.
    /// </summary>
    public class DishCountRM
    {
        public DishRM Dish { get; set; } = default!;

        public int? Count { get; set; }
    }
}
