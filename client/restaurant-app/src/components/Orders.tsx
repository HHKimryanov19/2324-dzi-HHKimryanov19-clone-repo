import { useEffect, useState } from 'react'
import Menu1 from './Menus/Menu1'
import OrderRM from '../models/Models/OrderRM'
import OrderService from '../services/order'
import '../style/orderStyle.css'
import { Link } from 'react-router-dom'
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';

type namesIds = {
  restaurantId: string,
  name: string,
}

function Orders() {
  const [orders, setOrders] = useState<OrderRM[]>([])
  const [names, setNames] = useState<namesIds[]>([])
  const [currentR, setR] = useState(``)
  const [startDate, setStartDate] = useState(``)
  const [endDate, setEndDate] = useState(``)

  const handleChange = (event: SelectChangeEvent) => {
    setR(event.target.value as string);
  };

  const status = (orderStatus: number) =>
  {
    var orderStatusStr = ''
    switch(orderStatus) 
    {
      case 0:
        orderStatusStr = 'Прекратена'
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
        orderStatusStr = 'Приета от доставчик'
      break;
      case 5:
        orderStatusStr = 'Доставена'
      break;
    }

    return orderStatusStr
  }

  useEffect(() =>{
    var os = new OrderService()

    if(currentR === '')
    {
      os.getOrdersByUserId().then((orders) => {
      if(startDate === '' || endDate === '')
      {
        setOrders(orders)
        setNamesFunction(orders)
      }
      else
      {
        setOrders(orders.filter((order) => {
          if((order.date >= startDate && order.date <= endDate) || order.date === null)
          {
            return order
          }
        }))
      }
    })
    }
    else
    {
      os.getOrdersByUserRestaurantId(currentR).then((orders) => {
        if(startDate === '' || endDate === '')
      {
        setOrders(orders)
        setNamesFunction(orders)
      }
      else
      {
        setOrders(orders.filter((order) => {
          if((order.date >= startDate && order.date <= endDate) || order.date === null)
          {
            return order
          }
        }))
      }
      })
    }
    
  }, [currentR, orders]);

  const setNamesFunction = (givenOrders: OrderRM[]) => {
    givenOrders.map((givenOrder) => {
      var givenName = {
        name: givenOrder.restaurant.name,
        restaurantId: givenOrder.restaurant.id
      }

      if(!names.find(name => name.restaurantId === givenOrder.restaurant.id))
      {
        setNames([...names, {restaurantId: givenName.restaurantId, name: givenName.name}]) 
      }
    })
  }

    return (
      <div  id="page">
        <Menu1></Menu1>
        <div id='mainContent'>
          <div id="orderList">
          <FormControl id="select" fullWidth>
            <InputLabel id="demo-simple-select-label">Ресторант</InputLabel>
              <Select
               value={currentR}
               label="Restaurant"
               onChange={handleChange}>
                {
                  names.map((name) => {
                    return (<MenuItem value={name.restaurantId}>{name.name}</MenuItem>)
                  })
                }
              </Select>
          </FormControl>
          <form>
            <label>Начална дата: </label>
            <input type='date' value={startDate} onChange={(event) => setStartDate(event.target.value)}></input>
            <label>Крайна дата: </label>
            <input type='date' value={endDate} onChange={(event) => setEndDate(event.target.value)}></input>
          </form>
          {
            orders.map((order) =>{
              return (
                <div className="orderCard" key={order.id}>
                  <div className='orderInfo'>
                    <p>{order.restaurant.name}</p>
                    {
                      order.date ? <p>Дата на поръчване: {order.date}</p> : null
                    }
                    {
                      order.deliveryDate ? <p>Дата на доставка: {order.deliveryDate}</p> : null
                    }
                    <p>{status(order.status)}</p>
                    <div className='orderCardButtonsDiv'>
                    <button className='buttonOrder'> <Link to="/order-dishes" onClick={() => localStorage.setItem('orderId', order.id)}>Повече</Link></button>
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