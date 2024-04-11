import HomeIcon from '@mui/icons-material/Home';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import PersonIcon from '@mui/icons-material/Person';
import MopedIcon from '@mui/icons-material/Moped';
import RestaurantIcon from '@mui/icons-material/Restaurant';
import LogoutIcon from '@mui/icons-material/Logout';
import '../../style/Menu1.css'
import { Link } from 'react-router-dom' 

function Menu1() {
  return (
    <div id='componentBody'>
    <div id="mainDiv">
		<div id="secondDiv">
			<Link id = "menuButtons" to="/home">
            <HomeIcon fontSize='large'></HomeIcon>
            <h3>Начало</h3>
			</Link>

			<Link id = "menuButtons" to="/restaurants">
            <RestaurantIcon fontSize='large'></RestaurantIcon>
            <h3>Ресторант</h3>
			</Link>

			<Link id = "menuButtons" to="/bookings">
            <MenuBookIcon fontSize='large'></MenuBookIcon>
            <h3>Резервации</h3>
			</Link>

			<Link id = "menuButtons" to="/orders">
            <MopedIcon fontSize='large'></MopedIcon>
            <h3>Поръчки</h3>
			</Link>
		</div>
        <div id="thirdDiv">
        <Link id = "menuButtons" to="/user/profile">
        <PersonIcon fontSize='large'></PersonIcon>
        <h3>Профил</h3>
		</Link>
        <Link id = "menuButtons" to="/" onClick={() => {
            localStorage.removeItem("token")
            localStorage.removeItem("roleCode")
        }}>
        <LogoutIcon fontSize='large'></LogoutIcon>
        <h3>Изход</h3>
		</Link>
        </div>
	</div>
    </div>
  )
}

export default Menu1