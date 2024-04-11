import Login from '../models/login.ts';
import Register from '../models/register.ts'

const base = "https://localhost:7277"

class AuthServices 
{
    async register(prop: Register)
    {
        const requestOptions = 
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(prop)
        }
        const response = await fetch(base + "/auth/user-register", requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async login(prop:Login) {
        const requestOptions = 
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(prop)
        }

        const response = await fetch(base + "/auth/user-login", requestOptions).then((response)=>{
            if(response.status < 400)
            {
                return response.json()
            }
        }).then((data) => {return data})
        return response;
    }

    async getRole()
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
        const response = await fetch(base + "/auth/user-role", requestOptions).then((response) => response.json()).then((data) => {return data});
        return response
    }
}

export default AuthServices