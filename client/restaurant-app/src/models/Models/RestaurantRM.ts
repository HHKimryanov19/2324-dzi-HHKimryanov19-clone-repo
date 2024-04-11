import Address from '../address'

type RestaurantRM = 
{
    id: string,
    name: string
    phone: string
    deliveryPrice: number
    address: Address,
    image: string
}

export default RestaurantRM;