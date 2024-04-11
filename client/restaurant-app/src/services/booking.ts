import BookingIM from "../models/InputModels/BookingIM";
import BookingVM from "../models/Models/BookingRM";

const base = "https://localhost:7277"

class BookingService
{
    async getBookingsByUserId(): Promise<BookingVM[]>
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
        var response = await fetch(base + "/bookings/get-allByUserId", requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async getBookingsByRestaurantId(restaurantId: string | null): Promise<BookingVM[]>
    {
        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+localStorage.getItem('token')
            },
        }

        if(restaurantId === null)
        {
            const response = await fetch(base + `/bookings/get-allByRestaurantId`, requestOptions).then((response) => response.json()).then((data) => {return data});
            return response;
        }
        else
        {
            const response = await fetch(base + `/bookings/get-allByRestaurantId/${restaurantId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
            return response;
        }
    }

    async getBooking(bookingId: string): Promise<BookingVM>
    {
        const requestOptions = 
        {
            method: 'GET',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+localStorage.getItem('token')
            },
        }
        const response = await fetch(base + `/booking/get-single/${bookingId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async AddBooking(booking: BookingIM, restaurantId: string): Promise<string>
    {
        const requestOptions = 
        {
            method: 'POST',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+ localStorage.getItem('token')
            },
            body: JSON.stringify(booking)
        }
        const response = await fetch(base + `/bookings/add/${restaurantId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async UpdateBooking(booking: BookingIM, bookingId: string)
    {
        const requestOptions = 
        {
            method: 'PUT',
            headers: 
            { 
                'Content-Type': 'application/json',
                'Authorization': 'bearer '+ localStorage.getItem('token')
            },
            body: JSON.stringify(booking)
        }
        const response = await fetch(base + `/bookings/update/${bookingId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }

    async RemoveBooking(bookingId: string)
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
        const response = await fetch(base + `/bookings/delete/${bookingId}`, requestOptions).then((response) => response.json()).then((data) => {return data});
        return response;
    }
}

export default BookingService;