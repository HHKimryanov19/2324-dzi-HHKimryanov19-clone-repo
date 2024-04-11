import { useEffect, useState } from "react"
import UserIM from "../../../models/InputModels/UserIM"
import UserService from "../../../services/user";

function ChangeForm() {
  const [userF, setUser] = useState<UserIM>({
    firstName: '',
    lastName:  '',
    password: '',
    email:  '',
    address: {
      country: '',
      city: '',
      street: '',
      number: ''
    }
  })

  var us = new UserService()

  useEffect(() => {
    us.get().then((data) => {
      setUser(data)
    })
  },[])

  

  function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = event.target;
    setUser({ ...userF, [name]: value });
  }

  function handleAddressChange(event: React.ChangeEvent<HTMLInputElement>) {
    var { name, value } = event.target;
        var newAddress = {
            country: userF.address.country,
            city: userF.address.city,
            street: userF.address.street,
            number: userF.address.number
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
        
        setUser({ ...userF, address: newAddress });
  }

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    us.update(userF as UserIM)
  }

    return (
        <div>
            <form className="formDiv" id="profileForms" onSubmit={handleSubmit}>
              <div>
                <input type="text" value={userF.firstName} name="firstName" placeholder="Първо име" onChange={handleInputChange}/>
                <input type="text" value={userF.lastName} name="lastName" placeholder="Фамилия" onChange={handleInputChange}/>
              </div>
              
               <div>
                <input type="text" value={userF.address.country} placeholder="Държава" name="country" onChange={handleAddressChange}/>
                <input type="text" value={userF.address.city} placeholder="Град" name="city" onChange={handleAddressChange}/>
              </div>

              <div>
                <input type="text" value={userF.address.street} placeholder="Улица" name="street" onChange={handleAddressChange}/>
                <input type="text" value={userF.address.number} placeholder="Номер" name="number" onChange={handleAddressChange}/>
              </div>
              <button className="cardButton" type="submit">Запази</button>
            </form>
        </div>
    )
}

export default ChangeForm