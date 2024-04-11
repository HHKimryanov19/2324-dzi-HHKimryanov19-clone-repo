import { useEffect, useState } from 'react'
import OrderRM from '../../models/Models/OrderRM'
import OrderService from '../../services/order'
import '../../style/orderStyle.css'
import { Link } from 'react-router-dom'

function Orders() {
  const [orders, setOrders] = useState<OrderRM[]>([])

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
        <div id='mainContent'>
          <div id="orderList">
          {
            orders.map((order) =>{
              return (
                <div className="orderCard" key={order.id}>
                  <div className='orderInfo'>
                    <p>Потребител: {order.userFullName}</p>
                    <p>Дата на поръчване: {order.date}</p>
                    <p>Адрес на доставка:{order.address}</p>
                    
                    <div className='orderCardButtonsDiv'>
                    <button className='buttonOrder'>
                    <Link to="/deliver/order-dishes" className='buttonOrder' onClick={() => localStorage.setItem('orderId', order.id)}>Повече</Link>
                    </button>
                    {
                        order.status === 3 ? <button type='button' className='buttonOrder' onClick={() => {
                            update(order.id, 4)
                            const change = orders.map(orderM => {
                                if (orderM.id == order.id) {
                                    orderM.status = 4
                                    return orderM
                                }
                                else
                                {
                                    return orderM
                                }
                            } )

                            setOrders(change)
                        }}>Приеми</button>: <button type='button' className='buttonOrder' onClick={() => {
                            update(order.id, 5)
                            const change = orders.filter(orderM => {
                                if (orderM.id !== order.id) {
                                    return orderM
                                }
                            } )
                            setOrders(change)
                        }}>Доставена</button>
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

export default Orders