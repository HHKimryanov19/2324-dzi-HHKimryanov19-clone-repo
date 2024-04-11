import Restaurant from "../models/Models/RestaurantRM";
import RestaurantIM from '../models/InputModels/RestaurantIM'

const base = "https://localhost:7277"

class RestaurantService
{
    async getByCity(): Promise<Restaurant[]>
    {

        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem("token") 
            },
        }

        var response =  await fetch(`${base}/restaurants/get-all`, requestOptions). then((response) => response.json()).then((data) => {return data});
        return response;
    } 

    async getRestaurants(restaurantId?: string | null): Promise<Restaurant>
    {
        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem("token") 
            },
        }
        var response = await fetch(`${base}/restaurants/get-single/${restaurantId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async AddRestaurant(restaurant: RestaurantIM, file: Blob | undefined): Promise<string>
    {
        const formData = new FormData();
        formData.append("Name", restaurant.name);
        formData.append("Phone", restaurant.phone);
        formData.append("DeliveryPrice", restaurant.deliveryPrice.toString());
        formData.append("Address.Country", restaurant.address.country);
        formData.append("Address.City", restaurant.address.city);
        formData.append("Address.Street", restaurant.address.street);
        formData.append("Address.Number", restaurant.address.number);
        if(file!==undefined)
        {
            formData.append("Image", file);
        }
        const requestOptions = 
        {
            method: 'POST',
            headers: 
            { 
                'Authorization': 'bearer ' + localStorage.getItem('token')
            },
            body: formData
        }
        const response = await fetch(base + `/restaurants/add`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async DeleteRestaurant(restaurantId: string)
    {
        const requestOptions = 
        {
            method: 'DELETE',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem('token')
            }
        }
        const response = await fetch(base + `/restaurants/delete/${restaurantId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async UpdateRestaurant(restaurant: RestaurantIM, restaurantId: string, file: Blob | undefined)
    {
        const formData = new FormData();
        formData.append("Name", restaurant.name);
        formData.append("Phone", restaurant.phone);
        formData.append("DeliveryPrice", restaurant.deliveryPrice.toString());
        formData.append("Address.Country", restaurant.address.country);
        formData.append("Address.City", restaurant.address.city);
        formData.append("Address.Street", restaurant.address.street);
        formData.append("Address.Number", restaurant.address.number);
        if(file!==undefined)
        {
            formData.append("Image", file);
        }
        const requestOptions = 
        {
            method: 'PUT',
            headers: 
            { 
                'Authorization': 'bearer ' + localStorage.getItem('token')
            },
            body: formData
        }
        const response = await fetch(base + `/restaurants/update/${restaurantId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }
}

export default RestaurantService;