import OrdersDishes from "./OrdersDishes"
import Restaurant from "./RestaurantRM"

type OrderRM = {
    id: string,
    status : number,
    date: string,
    deliveryDate : string,
    restaurantId: string,
    restaurant: Restaurant,
    address: string,
    userFullName: string,
    dishes: OrdersDishes[]
}

export default OrderRM