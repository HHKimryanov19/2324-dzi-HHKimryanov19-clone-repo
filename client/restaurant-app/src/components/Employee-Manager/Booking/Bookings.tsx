import MenuEM from '../../Menus/MenuEM'
import '../../../style/bookingStyle/bookingStyle.css'
import { useEffect, useState } from "react"
import BookingRM from "../../../models/Models/BookingRM"
import BookingService from "../../../services/booking"
import '../../../style/bookingStyle/bookingStyle.css'

function BookingsEM() {
  const [bookings, setBookings] = useState<BookingRM[]>([])

  useEffect(() => {
    var bs = new BookingService()
    bs.getBookingsByRestaurantId(null).then(bookings => setBookings(bookings))
  },[])

    return (
      <div id="page">
        <MenuEM></MenuEM>
        <div id="mainContent">
          <div id="bookingList">
            {
              bookings.map(booking =>
                {
                  return (
                    <div className="bookingCard" key={booking.id}>
                      <div className="bookingInfo">
                        <p>Име на потребителят: {booking.fullUserName}</p>
                      {
                        booking.isInside ? <p>Място на масата: Вътре</p> : <p>Място на масата: Вън</p>
                      }
                      <p>Дата: {booking.date}</p>
                      <p>Брой на хората: {booking.numberOfPeople}</p>
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

export default BookingsEM