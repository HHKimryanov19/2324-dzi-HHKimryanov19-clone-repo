import { useState, useEffect } from "react";
import '../../style/dishStyle.css'
import OrderService from "../../services/order";
import OrdersDishes from "../../models/Models/OrdersDishes";
import RestaurantService from "../../services/restaurant";
import RestaurantRM from "../../models/Models/RestaurantRM";
import '../../style/orderDish.css'
import '../../style/commonStyle/common.css'
import '../../style/dishStyle.css'
import MenuDeliver from "../Menus/MenuDeliver";

function DeliverOrderDishes() {
    const [orderDishes, setOrderDishes] = useState<OrdersDishes[]>([]);
    const [orderId] = useState(localStorage.getItem('orderId'))
    const [restaurant, setRestaurant] = useState<RestaurantRM>({
        id: "",
        name: "",
        phone: "",
        deliveryPrice: 0,
        address: {
            city: "",
            country: "",
            street: "",
            number: "",
        },
        image: "",
    })
    const [width, setWidth] = useState(window.innerWidth);
    var os = new OrderService();
    var rs = new RestaurantService();

    useEffect(() => {
    
    os.getOrderDishes(orderId).then((orderA) => { 
        os.getOrder(orderId).then((orderB) => {
            localStorage.setItem('orderRestaurant', orderB.restaurantId)
        })
        setOrderDishes(orderA)
    })

    rs.getRestaurants(localStorage.getItem('orderRestaurant')).then((restaurant) => { 
        setRestaurant(restaurant)
    })
      
    const handleResize = () => {
        setWidth(window.innerWidth);
      };
  
      window.addEventListener('resize', handleResize);
  
      return () => {
        window.removeEventListener('resize', handleResize);
      };

    }, []);

    const getPrice = () => {
        var price = 0;
        for (let i = 0; i < orderDishes.length;i++) 
        {
            price += orderDishes[i].count * orderDishes[i].dish.price
        }
        return price;
    }

    const getImage = (base64String: string) => {
        var image = new Image()
        image.src = "data:image/gif;base64," + base64String
        return image.src
    }

    return (
        <div>
            <MenuDeliver></MenuDeliver>
            <div id="dishBox">
                {
                    orderDishes.map((orderDish) => {
                        return (
                            <div className="dishCard" key={orderDish.dish.id}> 
                                <div id="dishInfo">
                                    {
                                        width > 500 ? <img id="image" src={getImage(orderDish.dish.imageBytes)} /> : null
                                    }                            
                                    <p>{orderDish.dish.title}</p>
                                    <p>{orderDish.dish.price}</p>
                                    
                                </div>
                                <div>
                                    <p className="countP">{orderDish.count}</p>
                                </div>
                            </div>
                        )
                    })
                }
                <div className="orderDishInfo">
                <p>Текуща цена: {getPrice()}</p>
                <p>Цена на доставка: {restaurant?.deliveryPrice}</p>
                <p id="final">Финална цена: {getPrice() + restaurant.deliveryPrice}</p>
                </div>
            </div>
        </div>
    )
}

export default DeliverOrderDishes