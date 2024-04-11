import { useState } from 'react'
import '../../../style/bookingStyle/bookingFormStyle.css'
import OrderService from '../../../services/order'

function DishForm(props: {dishId: string, dishInfo: string}) {
    const [dishCount, setDishCount] = useState(0)

      async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        var os = new OrderService();
        try
        {
            os.assignDish(props.dishId, dishCount);
        }
        catch (err)
        {
            console.error(err);
        } 
      }

    return (
      <>
      <form method="post" id="formUser" onSubmit={handleSubmit}>
        <p>{props.dishInfo}</p>
        <label>Бройка</label>
        <input type="number" name="place" value={dishCount} onChange={(e) => setDishCount(e.target.valueAsNumber)}></input>  
        <button type="submit" id='formButton'>Добави към поръчка</button>
      </form>
      </>
    )
}

export default DishForm