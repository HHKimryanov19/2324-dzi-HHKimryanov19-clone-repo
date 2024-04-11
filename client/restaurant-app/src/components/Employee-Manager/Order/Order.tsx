import MenuEM from '../../Menus/MenuEM'
import '../../../style/bookingStyle/bookingStyle.css'
import { useEffect, useState } from "react"
import OrderService from '../../../services/order'
import OrderRM from '../../../models/Models/OrderRM'
import { Link } from 'react-router-dom'
import '../../../style/bookingStyle/bookingStyle.css'

function OrdersEM() {
    const [orders, setOrders] = useState<OrderRM[]>([])


    const status = (orderStatus: number) =>
  {
    var orderStatusStr = ''
    switch(orderStatus) 
    {
      case 0:
        orderStatusStr = 'Отказано'
      break;
      case 1:
        orderStatusStr = 'Незавършена'
      break;
      case 2:
        orderStatusStr = 'Завършена'
      break;
      case 3:
        orderStatusStr = 'Чака да бъде събрана'
      break;
      case 4:
        orderStatusStr = 'Прието от доставчик'
      break;
      case 5:
        orderStatusStr = 'Доставено'
      break;
    }

    return orderStatusStr
  }
  
  useEffect(() =>{
    var os = new OrderService()
    os.getOrdersByRestaurantId().then((orders) => setOrders(orders))
  }, [])

  const update = (orderId: string, status: number) => {
    var os = new OrderService()
    os.getOrder(orderId).then((order) => { 
        order.status = status
        os.update(order, orderId).then(() => {})
    })
  }

    return (
      <div  id="page">
        <MenuEM></MenuEM>
        <div id='mainContent'>
          <div id="orderList">
          {
            orders.map((order) =>{
              return (
                <div className="orderCard" key={order.id}>
                  <div className='orderInfo'>
                    <p>Потребител: {order.userFullName}</p>
                    <p>Дата на поръчване: {order.date}</p>
                    <p>Адрес на доставка: {order.address}</p>
                    <p>{status(order.status)}</p>
                    <div className='orderCardButtonsDiv'>
                    <button className='buttonOrder'><Link to="/employee-manager/order/dishes" onClick={() => localStorage.setItem('orderId', order.id)}>Повече</Link></button>
                    {
                        order.status === 2 ? <button type='button' className='buttonOrder' onClick={() => {
                            update(order.id, 3)
                            const change = orders.map(orderM => {
                                if (orderM.id == order.id) {
                                    orderM.status = 3
                                    return orderM
                                }
                                else
                                {
                                    return orderM
                                }
                            } )

                            setOrders(change)
                        }}>Изчай за събиране</button> : null
                    }
                    </div>
                  </div>
                </div>
              )
            })
          }
          </div>
        </div>
      </div>
    )
}

export default OrdersEM