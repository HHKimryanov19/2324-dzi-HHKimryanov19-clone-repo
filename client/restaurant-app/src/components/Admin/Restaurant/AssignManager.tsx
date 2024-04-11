import { useState } from "react";
import EmployeeService from "../../../services/employee";

function AssignManager(props: {restaurantId: string}) {
    const [email, setEmail] = useState('')

    function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
        var { value } = event.target;
        setEmail(value)
    }

    async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        var es = new EmployeeService()
        await es.addManager(email, props.restaurantId)
      }

    return (
        <div>
            <p>За да добавиш мениджър към даденият ресторант добави имейлът му в полето</p>
            <form className="Form" onSubmit={handleSubmit}>
                <input type="text" id="emailInput" placeholder="Имейл" onChange={handleInputChange}></input>
                <button className="cardButton" type="submit">Добави</button>
            </form>
        </div>
    )
}

export default AssignManager