import { useState } from "react"
import UserService from "../../../services/user";
import Passwords from "../../../models/passwords";


function ChangePassword() {
  const [passwords, setUser] = useState<Passwords>({
   newPassword: '',
   oldPassword: ''
  })

  var us = new UserService();

  function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = event.target;
    setUser({ ...passwords, [name]: value });
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    await us.changePassword(passwords)
  }

    return (
        <div className="formDiv">
            <form  id="profileForms" onSubmit={handleSubmit}>
              <div>
                <input type="text" value={passwords.newPassword} name="newPassword" placeholder="Стара парола" onChange={handleInputChange}/>
                <input type="text" value={passwords.oldPassword} name="oldPassword" placeholder="Нова парола" onChange={handleInputChange}/>
              </div>
              
              <button className="cardButton" type="submit">Запази</button>
            </form>
        </div>
    )
}

export default ChangePassword