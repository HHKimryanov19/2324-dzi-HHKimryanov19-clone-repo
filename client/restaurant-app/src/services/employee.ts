import EmployeeVM from "../models/Models/EmployeeVM";

const base = "https://localhost:7277";

class EmployeeService
{
    async addEmployee(email: string): Promise<string>
    {
        const requestOptions = 
        {
            method: 'POST',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify(email)
        }
        const response = await fetch(base + `/employees/addEmployee`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async addManager(email: string, restaurantId: string): Promise<string>
    {
        const requestOptions = 
        {
            method: 'POST',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify(email)
        }
        const response = await fetch(base + `/employees/addManager/${restaurantId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async addDeliver(email: string): Promise<string>
    {
        const requestOptions = 
        {
            method: 'POST',
            headers:
            {
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify(email)
        }
        const response = await fetch(base + `/employees/addDeliver`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }
}

export default EmployeeService;