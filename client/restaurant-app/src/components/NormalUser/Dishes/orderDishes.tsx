import { useState, useEffect } from "react";
import '../../../style/dishStyle.css'
import OrderService from "../../../services/order";
import Menu1 from "../../Menus/Menu1";
import OrdersDishes from "../../../models/Models/OrdersDishes";
import CloseIcon from '@mui/icons-material/Close'
import { useNavigate } from 'react-router-dom'
import '../../../style/orderDish.css'
import OrderRM from "../../../models/Models/OrderRM";

function OrderDishes() {
    const [orderDishes, setOrderDishes] = useState<OrdersDishes[]>([]);
    const [order, setOrder] = useState<OrderRM>(
    {
        id: '',
        status : 1,
        date: '',
        deliveryDate : '',
        restaurantId: '',
        restaurant: {
            id: '',
            name: '',
            phone: '',
            deliveryPrice: 0,
            address: {
                country: '', 
                city: '',
                street: '',
                number: ''
            },
            image: '',
        },
        address:'',
        userFullName: '',
        dishes: []
    }
    )

    const [width, setWidth] = useState(window.innerWidth);
    const nav = useNavigate()
    var os = new OrderService();

    useEffect(() => {
    
    var orderId = localStorage.getItem('orderId')

    os.getOrderDishes(orderId).then((orderA) => { 
        os.getOrder(orderId).then((orderB) => {
            setOrder(orderB)
            console.log(orderB)
            localStorage.setItem('orderRestaurant', orderB.restaurantId)
        })
        setOrderDishes(orderA)
    })

    if(orderDishes.length == 0 && order?.status == -2){
        nav("/orders")
    }
      
    const handleResize = () => {
        setWidth(window.innerWidth);
      };
  
      window.addEventListener('resize', handleResize);
  
      return () => {
        window.removeEventListener('resize', handleResize);
      };

    }, [order]);

    const getPrice = () => {
        var price = 0;
        for (let i = 0; i < orderDishes.length;i++) 
        {
            price += orderDishes[i].count * orderDishes[i].dish.price
        }
        var deliveryPrice = order?.restaurant.deliveryPrice
        if(deliveryPrice === undefined)
        {
            return price;
        }
        else
        {
            return price + deliveryPrice
        }
        
    }

    const removeDish = (dishId: string) => {
        const remove = orderDishes.filter((orderDish) => orderDish.dish.id !== dishId);
        setOrderDishes(remove)
    }

    const getImage = (base64String: string) => {
        var image = new Image()
        image.src = "data:image/gif;base64," + base64String
        return image.src
      }

    const finish = () => {
        var orderI = {
            status: 2,
            date: order.date,
            deliveryDate: order.deliveryDate 
        } 
        os.update(orderI, order.id)
    }

    const update = (count:number, dishId: string) => {
        setOrderDishes(orderDishes => orderDishes.filter((ord) => {
            if(ord.dish.id === dishId)
            {
                ord.count = count
                return ord
            }
            else
            {
                return ord
            }
        }))

        var os = new OrderService()
        os.updateCount(order.id,dishId,count)
    }

    return (
        <div id="userOrders">
            <Menu1></Menu1>
            <div id="dishBox">
                {
                    orderDishes.map((orderDish) => {
                        return (
                            <div className="dishCard" key={orderDish.dish.id}> 
                                <div id="dishInfo">
                                    {
                                        width > 800 ? <img id="image" src={getImage(orderDish.dish.imageBytes)} /> : null
                                    }                            
                                    <p>{orderDish.dish.title}</p>
                                    <p>{orderDish.dish.price}</p>
                                </div>
                                {
                                    order?.status === 1 ? <div id="buttons">
                                    <div id="addRemoveButtons">
                                    <button type="button" id="countButtons" onClick={() => {
                                        update(orderDish.count - 1, orderDish.dish.id)
                                    }}>-</button>
                                        <p>{orderDish.count}</p>
                                    <button type="button" id="countButtons" onClick={() => {
                                        update(orderDish.count + 1, orderDish.dish.id)
                                    }}>+</button>
                                    </div>
                                    <button type="button" id="crossButton" onClick={() => {
                                        os.removeDish(order.id, orderDish.dish.id);
                                        removeDish(orderDish.dish.id)
                                        setOrder({...order, status: -2})
                                    }}><CloseIcon id='close'></CloseIcon></button>
                                    </div>: 
                                    <div>
                                        <p className="countP">{orderDish.count}</p>
                                    </div>
                                }
                            </div>
                        )
                    })
                }
                <div className="orderDishInfo">
                <p>Текуща цена: {getPrice()}</p>
                <p>Цена на доставка: {order?.restaurant.deliveryPrice}</p>
                <p id="final">Финална цена: {Number(getPrice() + order.restaurant.deliveryPrice).toFixed(2)}</p>
                {
                    order.status === 1 ?  <button type="button" className='buttonOrder' onClick={() => {
                        finish()
                    }}>Завърши</button> : null
                }
                </div>
            </div>
        </div>
    )
}

export default OrderDishes