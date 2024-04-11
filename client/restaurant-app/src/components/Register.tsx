import { useState } from 'react'
import '../style/Register.css'
import RegisterM from '../models/register'
import AuthServices from '../services/auth'
import { useNavigate } from 'react-router-dom'


function Register() {
  const [registerData, setData] = useState<RegisterM>
  ({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    address:{
      country: '',
      city: '',
      street: '',
      number: '',
    }
  })

  const auth = new AuthServices()
  const nav = useNavigate()

  function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = event.target;
    setData({ ...registerData, [name]: value });
  }

  localStorage.removeItem("token");

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    var result = await auth.register(registerData);
    if(result) {
      nav('/');
    }
  }
  
  return (
      <div id="registerComponentBody">
        <form action="POST" onSubmit={handleSubmit} id="RegisterForm">
         <h2>Регистрация</h2>
         <div>
         <input type="text" name="firstName" value={registerData?.firstName} onChange={handleInputChange} id = "inputfName" placeholder='Първо име'/>

         <input  type="text" name="lastName" value={registerData?.lastName} onChange={handleInputChange} placeholder='Фамилия'/>
         </div>

          <input type="email" name="email" value={registerData?.email} onChange={handleInputChange} placeholder='Имайл'/>

          <input type="password" name="password" value={registerData?.password} onChange={handleInputChange} placeholder='Парола'/>

          <button className="btnR" type="submit">Sign up</button>
        </form>
     </div>

  )
}

export default Register
