import { useState, useEffect } from "react";
import DishRM from "../../../models/Models/DishRM";
import DishService from "../../../services/dish";
import '../../../style/dishStyle.css'
import DishAddForm from './DishAddForm'
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import MenuEM from "../../Menus/MenuEM";
import '../../../style/employeeStyle.css'
import DishUpdateForm from "./DishUpdateForm"

function DishEM() {
    const [dishes, setDishes] = useState<DishRM[]>([])
    const [width, setWidth] = useState(window.innerWidth);
    const [dishId, setDishId] = useState('')
    const [openU, setOpenU] = useState(false)
    const [openA, setOpenA] = useState(false)

    useEffect(() => {
    var ds = new DishService();
    ds.getAllDishes().then(dishesA => {
      setDishes(dishesA)
      console.log(dishesA)
    });

    const handleResize = () => {
        setWidth(window.innerWidth);
      };
  
      window.addEventListener('resize', handleResize);
  
      return () => {
        window.removeEventListener('resize', handleResize);
      };

    }, []);

    const getImage = (base64String: string) => {
      var image = new Image()
      image.src = "data:image/gif;base64," + base64String
      return image.src
    }

    return (
        <div id="page">
          <MenuEM></MenuEM>
          <div id="mainContent">
            <div id="dishEmployee">
              <button type="button" className="cardButton" onClick={() => setOpenA(true)}>Добави</button>
              <Modal
                    open={openA}
                    onClose={() => setOpenA(false)}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description">                    
                        <Box id="box">
                          <DishAddForm></DishAddForm>
                        </Box>
                </Modal>
            </div>
            <div id="dishBox">
                {
                   dishes.map(dish => {
                    return (
                        <div className="dishCard" key={dish.id}> 
                            <div id="dishInfo">
                            {
                                width > 500 ? <img id="image" src={getImage(dish.imageBytes)}/> : null
                            }                            
                            <p>{dish.title}</p>
                            <p>{dish.price}</p>
                            </div>
                            <div id="buttons">
                                <button type="button" onClick={() => {
                                  setDishId(dish.id)
                                  setOpenU(true)
                                }}>Промени</button>
                                <button type="button" onClick={() => {
                                  var ds = new DishService()
                                  ds.removeDish(dish.id).then(() => {})
                                  setDishes(currentDish => {
                                    return currentDish.filter(dishR => dishR.id !== dish.id)
                                  })
                                }}>Премахни</button>
                            </div>
                        </div>
                    )
                   })
                }

                <Modal
                    open={openU}
                    onClose={() => setOpenU(false)}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description">                    
                        <Box id="box">
                            <DishUpdateForm dishId={dishId}></DishUpdateForm>
                        </Box>
                </Modal>
            </div>
          </div>
        </div>
    )
}

export default DishEM