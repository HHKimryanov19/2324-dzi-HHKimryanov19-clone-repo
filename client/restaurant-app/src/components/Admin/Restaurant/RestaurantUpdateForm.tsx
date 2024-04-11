import { useState } from "react";
import RestaurantIM from "../../../models/InputModels/RestaurantIM";
import RestaurantRM from "../../../models/Models/RestaurantRM";
import RestaurantService from "../../../services/restaurant";
import '../../../style/admin.css'

function RestaurantUpdateForm(props :{ RestaurantVM: RestaurantRM}) {
    const [file,setFile] = useState<Blob>() 
    const [restaurant, setRestaurant] = useState<RestaurantIM>({
        name: props.RestaurantVM.name,
        phone: props.RestaurantVM.phone,
        deliveryPrice: props.RestaurantVM.deliveryPrice,
        address: props.RestaurantVM.address
    });

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
        await rs.UpdateRestaurant(restaurant, props.RestaurantVM.id, file)
      }
    
    return (
        <div>
            <form className="Form" onSubmit={handleSubmit}>
            <input className='updateInput' type='file' name='image' onChange={(event) => {
                if(event.target.files !== null)
                {
                    setFile(new Blob([event.target.files[0]], {type: 'image/jpg'}))
                }
            }} accept=".jpg"/>
                <div id="twoInput">
                    <label>Име</label>
                    <input type="text" value={restaurant.name} name="name" onChange={handleInputChange} />
                    <label>Телефон</label>
                    <input value={restaurant.phone} id="secondInput" name="phone" onChange={handleInputChange} />
                </div>
                <div>
                <label>Цена на доставка</label>
                <input type='text' value={restaurant.deliveryPrice == 0 ? '' : restaurant.deliveryPrice} name="deliveryPrice" onChange={handleInputChange}></input>
                </div>
                <div id="twoInput">
                    <label>Държава</label>
                    <input type="text" value={restaurant.address.country} name="country" onChange={handAddressChange} />
                    <label>Град</label>
                    <input id="secondInput" type="text" value={restaurant.address.city} name="city" onChange={handAddressChange} />
                </div>
                <div id="twoInput">
                    <label>Улица</label>
                    <input type="text" value={restaurant.address.street} name="street" onChange={handAddressChange}></input>
                    <label>Номер</label>
                    <input id="secondInput" type="text" value={restaurant.address.number} name="number" onChange={handAddressChange}></input>
                </div>
                <button className="cardButton" type="submit">Промени</button>
            </form>
        </div>
    )
}
  
  export default RestaurantUpdateForm