import BookingIM from '../../../models/InputModels/BookingIM'
import BookingService from '../../../services/booking';
import '../../../style/bookingStyle/bookingFormStyle.css'
import '../../../style/alertStyle.css'
import { useState } from 'react'

function BookingForm(props: {restaurantId: string}) {
    const [booking,setBooking] = useState<BookingIM>({
      numberOfPeople: 1,
      date: '',
      isInside: false
    })

    function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
      const { name, value } = event.target;
      setBooking({ ...booking, [name]: value });
    }
  
    async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
      event.preventDefault();
      var bs = new BookingService();
      try
      {
        bs.AddBooking(booking, props.restaurantId)
      }
      catch (err) {
        console.log(err)
      }
    }

    return (
      <>
      <form action="POST" onSubmit={handleSubmit} id="formUser">
        <div>
        <label className="bookingLabel">Дата:</label>
        <input type="datetime-local" name="date" value={booking.date} onChange={handleInputChange}></input>
        </div>
        <div>
        <label className="bookingLabel">Брой на хората:</label>
        <input type="number" name="numberOfPeople" value={booking.numberOfPeople} onChange={handleInputChange} min="1" max="50"></input>
        </div>
          <div>
              <label>Място на масата: </label>
              <label htmlFor="inside">Вътре</label>
              <input type="radio" id="inside" name="place" onChange={()=>setBooking({ ...booking, isInside:true })}></input>
              <label htmlFor="outside">Вън</label>
              <input type="radio" id="outside" name="place" onChange={()=>setBooking({ ...booking, isInside:false })}></input>  
          </div>
        <button type="submit" id='formButton'>Резервирай</button>
      </form>
      </>
    )
}

export default BookingForm