import DishIM from "../models/InputModels/DishIM";
import DishVM from "../models/Models/DishRM";

const base = "https://localhost:7277"

class DishService
{
    async getAllDishes(restaurantId?: string, category?: number): Promise<DishVM[]>
    {
        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + localStorage.getItem('token')
            }
        }
        
        if(restaurantId != undefined)
        {
            if(category !== undefined)
            {
                const response = await fetch(base + `/dishes/get-all/${restaurantId}?category=${category}`, requestOptions).then((response) => response.json()).then((data) => {return data});
                return response
            }
            else
            {
                const response = await fetch(base + `/dishes/get-all/${restaurantId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
                return response
            }
        }
        else
        {
            const response = await fetch(base + `/dishes/get-all`, requestOptions).then((response) => response.json()).then((data) => {return data});
            return response
        }
    }

    async getDish(dishId: string): Promise<DishVM>
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
        const response = await fetch(base + `/dishes/get-single/${dishId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async addDish(dish: DishIM, file: Blob | undefined)
    {
        const formData = new FormData();
        formData.append("Title", dish.title);
        formData.append("Info", dish.info);
        formData.append("Price", dish.price.toString());
        formData.append("Category", dish.category.toString());
        if(file!==undefined)
        {
            formData.append("Image", file);
        }
        const requestOptions = 
        {
            method: 'POST',
            headers: 
            { 
                'Authorization': 'bearer '+localStorage.getItem('token')
            },
            body: formData
        }
        const response = await fetch(base + `/dishes/add`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async updateDish(dish: DishIM, file: Blob | undefined , dishId: string)
    {
        const formData = new FormData();
        formData.append("Title", dish.title);
        formData.append("Info", dish.info);
        formData.append("Price", dish.price.toString());
        formData.append("Category", dish.category.toString());
        if(file!==undefined)
        {
            formData.append("Image", file);
        }
        const requestOptions = 
        {
            method: 'PUT',
            headers: 
            { 
                'Authorization': 'bearer '+localStorage.getItem('token')
            },
            body: formData
        }
        const response = await fetch(base + `/dishes/update/${dishId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async removeDish(dishId:string)
    {
        const requestOptions = 
        {
            method: 'DELETE',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+localStorage.getItem('token')
            },
        }
        const response = await fetch(base + `/dishes/delete/${dishId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }
}

export default DishService;