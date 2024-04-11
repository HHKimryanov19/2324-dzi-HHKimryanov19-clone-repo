import { useState, useEffect } from "react";
import DishRM from "../models/Models/DishRM";
import DishService from "../services/dish";
import '../style/dishStyle.css'
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import DishForm from "./NormalUser/Dishes/dishesForm";

function Dishes() {
    const [dishes, setDishes] = useState<DishRM[]>([])
    const [dishCount, setCount] = useState(-1)
    const [category,setCategory] = useState(0)
    const [width, setWidth] = useState(window.innerWidth);
    const [open, setOpen] = useState(false);
    const [dishId, setDishId] = useState('');
    const [dishInfo, setDishInfo] = useState('');

    useEffect(() => {
    var ds = new DishService();
    var restaurantId = localStorage.getItem("currentRestaurantId")?.toString();
    ds.getAllDishes(restaurantId, category).then(dishes => {
        setDishes(dishes)
        if(dishCount == -1)
        {
            setCount(dishes.length)
        }
    });

    const handleResize = () => {
        ds.getAllDishes(restaurantId, category).then(dishes => setDishes(dishes));
        setWidth(window.innerWidth);
      };
  
      window.addEventListener('resize', handleResize);
  
      return () => {
        window.removeEventListener('resize', handleResize);
      };

    }, [category]);

    const getImage = (base64String: string) => {
        var image = new Image()
        image.src = "data:image/gif;base64," + base64String
        return image.src
      }

    return (
        <div id="dishBox">
            {
                dishCount !== 0 ? 
            <form>
                <label>Категория: </label>

                <input type="radio" id="outside" name="category" onChange={()=>setCategory(1)}></input>
                <label htmlFor="sea">Морски</label>

                <input type="radio" id="outside" name="category" onChange={()=>setCategory(2)}></input>
                <label htmlFor="meat">Месо</label>

                <input type="radio" id="inside" name="category" onChange={()=>setCategory(3)}>
                </input><label htmlFor="vegetarian">Вегетарианаски</label>
                
                
                <input type="radio" id="outside" name="category" onChange={()=>setCategory(4)}>
                </input><label htmlFor="dessert">Десерт</label>
                
                <input type="radio" id="inside" name="category" onChange={()=>setCategory(5)}></input>
                <label htmlFor="pizza">Пица</label>

                <input type="radio" id="outside" name="category" onChange={()=>setCategory(6)}></input>
                <label htmlFor="salad">Салата</label>
            </form> : null
            }
                {
                   dishes.map(dish => {
                    return (
                        <div className="dishCard" key={dish.id}> 
                            <div id="dishInfo">
                            {
                                width > 800 ? <img id="image" src={getImage(dish.imageBytes)} /> : null
                            }                            
                            <p>{dish.title}</p>
                            <p>{dish.price}</p>
                            </div>
                            <div id="buttons">
                                <button onClick={() => {
                                    setDishId(dish.id)
                                    setDishInfo(dish.info)
                                    setOpen(true)
                                }}>Добави</button>
                            </div>
                        </div>
                    )
                   })
                }

                <Modal
                    open={open}
                    onClose={() => setOpen(false)}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description">                    
                        <Box id="box">
                            <DishForm dishId={dishId} dishInfo={dishInfo}></DishForm>
                        </Box>
                </Modal>
        </div>
    )
}

export default Dishes