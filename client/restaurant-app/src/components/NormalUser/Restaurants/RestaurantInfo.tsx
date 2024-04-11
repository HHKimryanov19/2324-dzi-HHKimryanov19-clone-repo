import { useEffect, useState } from "react";
import RestaurantRM  from '../../../models/Models/RestaurantRM.ts'
import RestaurantService from "../../../services/restaurant.ts";
import Dishes from "../../Dishes.tsx";
import Menu1 from "../../Menus/Menu1.tsx"
import '../../../style/restaurantInfo.css'
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import BookingForm from '../Bookings/BookingForm.tsx'

function RestaurantInfo() {
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const [restaurant, setRestaurant] = useState<RestaurantRM>({
        id: "",
        name: "",
        phone: "",
        deliveryPrice: 1.50,
        address: {
            city: "",
            country: "",
            street: "",
            number: "",
        },
        image: "",
    })

    useEffect(() => {
          var services = new RestaurantService();
          services.getRestaurants(localStorage.getItem('currentRestaurantId')).then(apiRestaurant => setRestaurant(apiRestaurant))
    },[])

    const getImage = (base64String: string) => {
        var image = new Image()
        image.src = "data:image/gif;base64," + base64String
        return image.src
      }

    return (
        <div>
            <Menu1 />
            <div className="info">
                <div id="mainPart">
                <img id="backgroundIMG" src={getImage(restaurant.image)} />
                <div id="infoText">
                <h3 id="pInfo">{restaurant.name}</h3>
                <h2 id="pInfo">{restaurant.address.street} {restaurant.address.number}, {restaurant.address.city}</h2>
                <p id="pInfo">Телефон: {restaurant.phone}</p> 
                <p id="pInfo">Цена на доставка: {restaurant.deliveryPrice}</p>
                <div>
                        <button className="cardButton bookButton" onClick={handleOpen}>Резервация</button>
                    <Modal
                        open={open}
                        onClose={handleClose}
                        aria-labelledby="modal-modal-title"
                        aria-describedby="modal-modal-description">

                        <Box id="box">
                            <BookingForm restaurantId={restaurant.id}></BookingForm>
                        </Box>

                    </Modal>
                    </div>
                
                </div>
                </div>
                
                <Dishes/>
            </div>
            
        </div>
    )
}

export default RestaurantInfo