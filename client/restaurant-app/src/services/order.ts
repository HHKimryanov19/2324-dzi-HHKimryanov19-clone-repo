import OrderIM from "../models/InputModels/OrderIM";
import OrderRM from "../models/Models/OrderRM";
import OrdersDishes from "../models/Models/OrdersDishes";

const base = "https://localhost:7277"

class OrderService
{
    async getOrder(orderId: string | null): Promise<OrderRM>
    {
        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+ localStorage.getItem('token')
            },
        }
        var response = await fetch(base + `/orders/get/${orderId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async getOrdersByUserId(): Promise<OrderRM[]>
    {
        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+ localStorage.getItem('token')
            },
        }
        var response = await fetch(base + `/orders/get-ByUserId`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async getOrdersByRestaurantId(): Promise<OrderRM[]>
    {
        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+ localStorage.getItem('token')
            },
        }
        var response = await fetch(base + `/orders/get-ByRestaurantId`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async getOrdersByUserRestaurantId(restaurantId: string): Promise<OrderRM[]>
    {
        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+ localStorage.getItem('token')
            },
        }
        var response = await fetch(base + `/orders/get-ByUserRestaurantId/${restaurantId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async assignDish(dishId: string, dishCount: number)
    {
        const requestOptions = 
        {
            method: 'POST',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+ localStorage.getItem('token')
            },
            body: JSON.stringify(dishCount)
        }
        var response = await fetch(base + `/orders/assignDish/${dishId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async removeDish(orderId: string | null, dishId: string)
    {
        const requestOptions = 
        {
            method: 'DELETE',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+ localStorage.getItem('token')
            },
        }
        var response = await fetch(base + `/orders/removeDish/${orderId}/${dishId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async updateCount(orderId: string, dishId: string, count: number)
    {
        const requestOptions = 
        {
            method: 'PUT',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+ localStorage.getItem('token')
            },
            body: JSON.stringify(count)
        }
        var response = await fetch(base + `/orders/updateCount/${orderId}/${dishId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }


    async update(order: OrderIM, orderId: string | null)
    {

        const requestOptions = 
        {
            method: 'PUT',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify(order)
        }

        var response = await fetch(base + `/orders/update/${orderId}`, requestOptions).then((response) => {
            response.json()
        }).then((data) => {return data});
        return response;
    }

    async getOrderDishes(orderId: string | null): Promise<OrdersDishes[]>
    {
        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+ localStorage.getItem('token')
            },
        }
        var response = await fetch(base + `/orders/getOrderDishes/${orderId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }
}

export default OrderService