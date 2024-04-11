import { useEffect, useState } from "react"
import RestaurantRM from "../../models/Models/RestaurantRM"
import RestaurantService from "../../services/restaurant"
import RestaurantAdminGrid from "./Restaurant/RestaurantAdminGrid"
import '../../style/commonStyle/common.css'
import '../../style/admin.css'
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import RestaurantAdd from "./Restaurant/RestaurantForm"
import MenuAdmin from "../Menus/MenuAdmin"

function StartPage() {
    const [restaurants, setRestaurants] = useState<RestaurantRM[]>([]) 
    const [open, setOpen] = useState(false)
    useEffect(() =>{
        var rs = new RestaurantService()
        rs.getByCity().then((restaurants) => setRestaurants(restaurants))
    }, [])

    return (
        <div>
            <MenuAdmin></MenuAdmin>
            <div id="homeAdmin">
                <div id="adminPage">
                    <button type="button" className="cardButton" onClick={()=>setOpen(true)}>Добави нов</button>
                </div>
                <RestaurantAdminGrid Restaurants={restaurants}></RestaurantAdminGrid>

                <Modal
                open={open}
                onClose={()=>setOpen(false)}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description">

                <Box id="box">
                    <RestaurantAdd></RestaurantAdd>
                </Box>

                </Modal>
            </div>
        </div>
    )
}

export default StartPage