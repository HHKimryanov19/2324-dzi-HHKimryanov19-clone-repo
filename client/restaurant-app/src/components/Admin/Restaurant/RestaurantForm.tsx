import { useState } from "react"
import RestaurantIM from "../../../models/InputModels/RestaurantIM"
import RestaurantService from "../../../services/restaurant";
import '../../../style/admin.css'

function RestaurantAdd() {
    const [file,setFile] = useState<Blob>() 
    const [restaurant, setRestaurant] = useState<RestaurantIM>({
        name: '',
        phone:'',
        deliveryPrice: 0,
        address: {
            country:'',
            city: '',
            street: '',
            number: ''
        }
    })

    function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
        var { name, value } = event.target;
        setRestaurant({ ...restaurant, [name]: value });
    }
    
    function handAddressChange(event: React.ChangeEvent<HTMLInputElement>) {
        var { name, value } = event.target;
        var newAddress ={
            country: restaurant.address.country,
            city: restaurant.address.city,
            street: restaurant.address.street,
            number: restaurant.address.number
        }
        switch(name)
        {
            case "country":
                newAddress.country = value
            break;
            case "city": 
                newAddress.city = value
            break;
            case "street": 
                newAddress.street = value
            break;
            case "number": 
                newAddress.number = value
            break;
        }
        
        setRestaurant({ ...restaurant, address: newAddress });
      }

      async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        var rs = new RestaurantService()
        await rs.AddRestaurant(restaurant, file)
      }
    
    return (
        <div>
            <form className="Form" onSubmit={handleSubmit} encType="multipart/form-data">
            <input className='updateInput' type='file' name='image' onChange={(event) => {
                if(event.target.files !== null)
                {
                    setFile(new Blob([event.target.files[0]]))
                }
            }} />
                <div id="twoInput" className="addInput">
                    <input type="text" value={restaurant.name} placeholder="Име" name="name" onChange={handleInputChange} />
                    <input value={restaurant.phone} placeholder="Телефон" name="phone" onChange={handleInputChange} />
                </div>
                <input type='text' value={restaurant.deliveryPrice == 0 ? '' : restaurant.deliveryPrice} placeholder="Delivery price" name="deliveryPrice" onChange={handleInputChange}></input>
                <div id="twoInput" className="addInput">
                    <input type="text" value={restaurant.address.country} placeholder="Държава" name="country" onChange={handAddressChange} />
                    <input type="text" value={restaurant.address.city} placeholder="Град" name="city" onChange={handAddressChange} />
                </div>
                <div id="twoInput" className="addInput">
                    <input type="text" value={restaurant.address.street} placeholder="Улица" name="street" onChange={handAddressChange}></input>
                    <input type="text" value={restaurant.address.number} placeholder="Номер" name="number" onChange={handAddressChange}></input>
                </div>
                <button className="cardButton" type="submit">Add</button>
            </form>
        </div>
    )
}
  
  export default RestaurantAdd