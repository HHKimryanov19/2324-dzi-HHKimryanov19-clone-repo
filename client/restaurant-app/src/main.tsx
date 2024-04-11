import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './components/App.tsx'
import { RouterProvider } from 'react-router-dom'
import { createBrowserRouter } from 'react-router-dom'
import Login  from './components/Login.tsx'
import Register  from './components/Register.tsx'
import MainPage from './components/NormalUser/MainPage.tsx'
import RestaurantInfo from './components/NormalUser/Restaurants/RestaurantInfo.tsx'
import Orders from './components/Orders.tsx'
import Bookings from './components/NormalUser/Bookings/Bookings.tsx'
import Profile from './components/NormalUser/ProfilePages/Profile.tsx'
import Restaurants from './components/NormalUser/Restaurants/Restaurants.tsx'
import OrderDishes from './components/NormalUser/Dishes/orderDishes.tsx'
import StartPage from './components/Admin/StartPage.tsx'
import StartDPage from './components/Deliver/StartDPage.tsx'
import DeliverOrderDishes from './components/Deliver/OrderDishes.tsx'
import BookingsEM from './components/Employee-Manager/Booking/Bookings.tsx'
import OrdersEM from './components/Employee-Manager/Order/Order.tsx'
import DishesEM from './components/Employee-Manager/Dishes/Dishes.tsx'
import EmployeeOrderDishes from './components/Employee-Manager/Order/orderDishes.tsx'
import ProfileEM from './components/Employee-Manager/Profile.tsx'
import ProfileAdmin from './components/Admin/AdminProfile.tsx'
import ProfileDeliver from './components/Deliver/DeliverProfile.tsx'

const router = createBrowserRouter([
  {
      path: '/',
      element: <App />,
  },
  {
    path: '/admin',
    element: <StartPage />,
  },
  {
    path: '/admin/profile',
    element: <ProfileAdmin />,
  },
  {
    path: '/deliver/order-dishes',
    element: <DeliverOrderDishes />,
  },
  {
    path: '/user',
    element: <MainPage />,
  },
  {
    path: '/employee-manager/booking',
    element: <BookingsEM />,
  },
  {
    path: '/deliver/profile',
    element: <ProfileDeliver />,
  },
  {
    path: '/employee-manager/orders',
    element: <OrdersEM />,
  },
  {
    path: '/employee-manager/dishes',
    element: <DishesEM />,
  },
  {
    path:'/register',
    element: <Register />
  },
  {
    path:'/login',
    element: <Login />
  },
  {
    path: '/restaurant',
    element: <RestaurantInfo />,
  },
  {
    path: '/deliver-page',
    element: <StartDPage />
  },
  {
    path: '/restaurants',
    element: <Restaurants />,
  },
  {
      path: '/home',
      element: <MainPage />,
  },
  {
    path: '/order-dishes',
    element: <OrderDishes />,
  },
  {
    path: '/orders',
    element: <Orders />
  },
  {
    path: '/bookings',
    element: <Bookings />
  },
  {
    path: '/user/profile',
    element: <Profile />
  },
  {
    path: '/employee/profile',
    element: <ProfileEM />
  },
  {
    path: '/employee-manager/order/dishes',
    element: <EmployeeOrderDishes />
  }
])

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
)
