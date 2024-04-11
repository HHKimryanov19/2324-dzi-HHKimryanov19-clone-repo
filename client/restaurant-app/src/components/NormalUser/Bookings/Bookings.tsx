import Menu1 from "../../Menus/Menu1"
import '../../../style/bookingStyle/bookingStyle.css'
import { useEffect, useState } from "react"
import BookingRM from "../../../models/Models/BookingRM"
import BookingService from "../../../services/booking"
import '../../../style/bookingStyle/bookingStyle.css'
import CloseIcon from '@mui/icons-material/Close'

function Bookings() {
  const [bookings, setBookings] = useState<BookingRM[]>([])

  useEffect(() => {
    var bs = new BookingService()
    bs.getBookingsByUserId().then(bookings => {
      setBookings(bookings)
    })
  },[])

    return (
      <div id="page">
        <Menu1></Menu1>
        <div id="mainContent">
          <div id="bookingList">
            {
              bookings.map(booking =>
                {
                  return (
                    <div className="bookingCard" key={booking.id}>
                      <button type="button" onClick={()=>{
                        var bs = new BookingService()
                        bs.RemoveBooking(booking.id)
                        setBookings(currentBookings => {
                          return currentBookings.filter(bookingL => bookingL.id !== booking.id)
                        })
                      }}><CloseIcon></CloseIcon></button>
                      <div className="bookingInfo">

                      <p>Име на ресторантът : {booking.restaurantName}</p>
                      {
                        booking.isInside ? <p>Място на масата: Вътре</p> : <p>Място на масата: Вън</p>
                      }
                      <p>Дата: {booking.date}</p>
                      <p>Брой на присъстващите: {booking.numberOfPeople}</p>
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

export default Bookings