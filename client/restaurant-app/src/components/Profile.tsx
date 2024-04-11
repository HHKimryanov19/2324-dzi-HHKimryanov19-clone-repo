import '../style/profileStyle.css'
import ChangeForm  from './NormalUser/ProfilePages/ChangeForm'
import ChangePassword from "./NormalUser/ProfilePages/ChangePassword"
import UserService from "../services/user";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import EmployeeService from '../services/employee';

function Profile() {
  const [email, setEmail] = useState('')
  const [emailE, setEmailE] = useState('')
  const [emailD, setEmailD] = useState('')

  const nav = useNavigate();
  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    var us = new UserService()
    var result = await us.delete(email)
    if(!result) 
    {
      localStorage.removeItem('token')
      nav('/')
    }
  }
    return (
        <div id="mainContent">
          <div id="formsDiv">
            <ChangeForm></ChangeForm>
            <ChangePassword></ChangePassword>
            <form id="profileForms" className="formDiv emailsForms" onSubmit={handleSubmit}>
              <input value={email} placeholder="Въведенете имейлът за да изтриете профилът" onChange={(event) => {setEmail(event.target.value)}}></input>
              <button className="cardButton" type="submit">Изтрии профилът</button>
            </form>
            {
              Number(localStorage.getItem("roleCode")) === 3 ? 
              <div>
                <form id="profileForms" className="formDiv emailsForms" onSubmit={() => {
                  var es = new EmployeeService()
                  es.addEmployee(emailE)
                }}>
                  <input value={emailE} placeholder="Въведи имейлът на потребителят" onChange={(event) => {setEmailE(event.target.value)}}></input>
                  <button className="cardButton" type="submit">Добави служител</button>
                </form>

                <form id="profileForms" className="formDiv emailsForms" onSubmit={() => {
                  var es = new EmployeeService()
                  es.addDeliver(emailD)
                }}>
                  <input value={emailD} placeholder="Въведи имейлът на потребителят" onChange={(event) => {setEmailD(event.target.value)}}></input>
                  <button className="cardButton" type="submit">Добваи доставчик</button>
                </form>
              </div>
              : null
            }
          </div>
        </div>
    )
}

export default Profile