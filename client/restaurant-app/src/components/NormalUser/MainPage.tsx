import { useEffect, useState } from 'react';
import Menu1 from '../Menus/Menu1'
import RestaurantService from '../../services/restaurant'
import RestaurantRM from "../../models/Models/RestaurantRM"
import "../../style/mainPage.css"
import RestaurantsGrid from './Restaurants/RestaurantsGrid';
import Carousel from "nuka-carousel"


function MainPage() {
    const [restaurants, setRestaurants] = useState<RestaurantRM[]>([])

    useEffect(() => {
    var rs = new RestaurantService();
    rs.getByCity().then(restaurants => setRestaurants(restaurants.slice(0,10)));
    }, []);

    return (
        <div id="page">
            <Menu1></Menu1>
            <div id="startDiv">
            <div id='carousel'>
            <Carousel>
                <img src="https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTA1L3Vwd2s2MTY2MTU3Ny13aWtpbWVkaWEtaW1hZ2Uta293YXBlZWouanBn.jpg" />
                <img src="https://media.licdn.com/dms/image/C4E12AQFHL3tTcJ155Q/article-cover_image-shrink_720_1280/0/1520096242009?e=2147483647&v=beta&t=ZKOB7gnbv-wNI9_qtV3Sfqx_b8kZeiknfi2qdKc0UOE" />
                <img src="https://media.istockphoto.com/id/487077896/photo/close-up-of-fast-food-snacks-and-drink-on-table.jpg?s=612x612&w=0&k=20&c=kjSfS7tuG1pQAg6_FvBeji-l3pgQ3DckRS4wpsC8lCY=" />
            </Carousel>
            </div>
            <RestaurantsGrid Restaurants={restaurants}></RestaurantsGrid>
            </div>
        </div>
    )
}

export default MainPage