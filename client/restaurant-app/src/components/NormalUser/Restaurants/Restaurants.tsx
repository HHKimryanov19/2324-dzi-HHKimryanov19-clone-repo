import Menu1 from "../../Menus/Menu1"
import RestaurantsGrid from "./RestaurantsGrid"
import Restaurant from "../../../models/Models/RestaurantRM";
import { useState, useEffect } from "react";
import RestaurantService from "../../../services/restaurant";

function Restaurants() {
  const [restaurants, setRestaurants] = useState<Restaurant[]>([])

    useEffect(() => {
    var rs = new RestaurantService();
    rs.getByCity().then(restaurants => setRestaurants(restaurants));
    }, []);

    localStorage.removeItem('currentRestaurantId')

    return (
      <div  id="page">
        <Menu1></Menu1>
        <RestaurantsGrid Restaurants={restaurants}></RestaurantsGrid>
      </div>
    )
}

export default Restaurants