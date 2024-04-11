using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Shared
{
    public static class ExceptionMessages
    {
        public const string UserMessage = "User doesn't exist";

        public const string InvalidCreditinalsMessage = "Invalid creditinals";

        public const string BookingMessage = "Booking/s doesn't/don't exist";

        public const string RestaurantMassage = "Restaurant/s doesn't/don't exist";

        public const string OrderMessage = "Order/s doesn't/don't exist";

        public const string DishMessage = "Dish/es doesn't/don't existver";

        public const string RestaurantIdDishMassage = "This user doesn't have same restaurantId as the dish";

        public const string UserIdOrderMessage = "This user doesn't have same userId as the order";

        public const string RestaurantIdOrderMassage = "This restaurant doesn't have same restaurantId as the order";

        public const string WantedEntity = "Wanted entity/ies doesn't/don't exist";
    }
}
