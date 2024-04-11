import UserIM from "../models/InputModels/UserIM";
import UserVM from "../models/Models/UserRM";
import Passwords from "../models/passwords";
const base = "https://localhost:7277"

class UserService
{
    async update(user: UserIM) : Promise<UserVM>
    {
        const requestOptions = 
        {
            method: 'PUT',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify(user)
        }
        const response = await fetch(base + `/users/update`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }


    async get()
    {
        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem('token')
            },
        }
        const response = await fetch(base + `/users/info`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async changePassword(passwords: Passwords)
    {
        const requestOptions = 
        {
            method: 'PUT',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify(passwords)
        }
        const response = await fetch(base + `/users/changePassword`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async delete(email: string)
    {
        const requestOptions = 
        {
            method: 'DELETE',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify(email)
        }
        const response = await fetch(base + `/users/delete`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }
}

export default UserService;