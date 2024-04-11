import RestaurantRM from "../../../models/Models/RestaurantRM"
import {Link} from "react-router-dom"

function RestaurantsGrid(props : {Restaurants: RestaurantRM[]}) {

    const getImage = (base64String: string) => {
        var image = new Image()
        image.src = "data:image/gif;base64," + base64String
        return image.src
      }

    return (
        <div id="containerR">
            {
                props.Restaurants.map(restaurant => {
                    return (
                    <div id="singleR" key={restaurant.id}>
                        <div id="cardImage">
                            <Link to="/restaurant"><img src={getImage(restaurant.image)} /></Link>
                        </div>
                        <div id = "info">
                        <p id = "title">{restaurant.name}</p>
                        <p id = "subtitle">{restaurant.address.city}</p>
                        <div className='cardButtonDiv'>
                        <Link className="cardButton" to={"/restaurant"} onClick={() => localStorage.setItem('currentRestaurantId',restaurant.id)}>Повече</Link>
                        </div>
                        </div>
                    </div>
                    )
                })
            }
            </div>
    )
}
  
  export default RestaurantsGrid