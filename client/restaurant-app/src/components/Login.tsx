import { useState } from 'react'
import '../style/Login.css' 
import LoginModel from '../models/login'
import AuthServices from '../services/auth'
import { useNavigate } from 'react-router-dom';

function Login() {
  const auth = new AuthServices();
  const nav = useNavigate()

  const [loginModel, setModel] = useState<LoginModel>
  ({
    email: '',
    password: '',
  })

  function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = event.target;
    setModel({ ...loginModel, [name]: value });
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    var result = await auth.login(loginModel)
    if(result !== '')
    {
      localStorage.setItem('token', result)
      var roleResult = await auth.getRole()
      
      localStorage.setItem('roleCode', roleResult)
      switch(Number(roleResult))
      {
        case 0:
          nav('/admin')
        break;

        case 1:
          nav('/home')
        break;

        case 2:
          nav('/employee-manager/orders')
        break;

        case 3:
          nav('/employee-manager/orders')
        break;

        case 4:
          nav('/deliver-page')
        break;
      }
    }
  }

  return (
    
      <div className="container">
        <form action="" onSubmit={handleSubmit} id="LoginForm">
        <h2>Вход</h2>
        <input className="loginInput" type="email" name='email' value={loginModel.email} onChange={handleInputChange} placeholder='Имейл'/>
        <input className="loginInput" type="password" name='password' value={loginModel.password} onChange={handleInputChange} placeholder='Парола'/>

        <button className="btnR" type="submit">Влез</button>
      </form>
      </div>
  )
}

export default Login
