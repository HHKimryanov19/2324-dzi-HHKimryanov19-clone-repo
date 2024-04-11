import { useState } from "react"
import RestaurantRM from "../../../models/Models/RestaurantRM"
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import RestaurantUpdateForm from "./RestaurantUpdateForm";
import RestaurantService from "../../../services/restaurant";
import AssignManager from "./AssignManager";
import '../../../style/admin.css'

function RestaurantAdminGrid(props : {Restaurants: RestaurantRM[]}) {
    const [openU, setOpenU] = useState(false)
    const [openA, setOpenA] = useState(false)
    const [restaurantId, setId] = useState('')
    const [restaurantM, setRestaurant] = useState<RestaurantRM>({
        id: '',
        name: '',
        phone:'',
        deliveryPrice: 0,
        address: {
            country:'',
            city: '',
            street: '',
            number: ''
        },
        image: ''
    })

    const getImage = (base64String: string) => {
        var image = new Image()
        image.src = "data:image/gif;base64," + base64String
        return image.src
      }

    return (
            <div id="containerAdmin">
            {
                props.Restaurants.map(restaurant => {
                    return (
                    <div id="singleR" key={restaurant.id}>
                        <div id="cardImage">
                            <img src={getImage(restaurant.image)} />
                        </div>
                        <div id = "info">
                            <p id = "title">{restaurant.name}</p>
                            <p id = "subtitle">{restaurant.address.city}</p>
                            <div className='cardButtonDiv'>
                                <button type='button' className="cardButton"  onClick={() => {
                                    setOpenU(true)
                                    setRestaurant(restaurant)
                                }}>Промени</button>
                            </div>
                            <div className='cardButtonDiv'>
                                <button className="cardButton" type="button" onClick={() => {
                                    var rs = new RestaurantService()
                                    rs.DeleteRestaurant(restaurant.id)
                                }}>Премахни</button>
                            </div>
                            <div className='cardButtonDiv'>
                                <button type='button' className="cardButton"  onClick={() => {
                                    setOpenA(true)
                                    setId(restaurant.id)
                                }}>Добави мениджър</button>
                            </div>
                        </div>
                    </div>
                    )
                })
            }
            <Modal
            open={openA}
            onClose={()=>setOpenA(false)}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description">

            <Box id="box">
                <AssignManager restaurantId = {restaurantId}></AssignManager>
            </Box>

            </Modal>

            <Modal
            open={openU}
            onClose={()=>setOpenU(false)}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description">

            <Box id="box">
                <RestaurantUpdateForm RestaurantVM={restaurantM}></RestaurantUpdateForm>
            </Box>

            </Modal>
            </div>
    )
}
  
  export default RestaurantAdminGrid