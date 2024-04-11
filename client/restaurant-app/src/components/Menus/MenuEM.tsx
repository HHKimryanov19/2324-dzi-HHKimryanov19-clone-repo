import MenuBookIcon from '@mui/icons-material/MenuBook';
import PersonIcon from '@mui/icons-material/Person';
import MopedIcon from '@mui/icons-material/Moped';
import LunchDiningIcon from '@mui/icons-material/LunchDining';
import LogoutIcon from '@mui/icons-material/Logout';
import '../../style/Menu1.css'
import { Link } from 'react-router-dom' 

function MenuEM() {
  return (
    <div id='componentBody'>
    <div id="mainDiv">
		<div id="secondDiv">
			<Link id = "menuButtons" to="/employee-manager/booking">
            <MenuBookIcon fontSize='large'></MenuBookIcon>
            <h3>Резервации</h3>
			</Link>

			<Link id = "menuButtons" to="/employee-manager/orders">
            <MopedIcon fontSize='large'></MopedIcon>
            <h3>Поръчки</h3>
			</Link>

            <Link id = "menuButtons" to="/employee-manager/dishes">
            <LunchDiningIcon fontSize='large'></LunchDiningIcon>
            <h3>Ястия</h3>
			</Link>
		</div>
        <div id="thirdDiv">
        <Link id = "menuButtons" to="/employee/profile">
        <PersonIcon fontSize='large'></PersonIcon>
        <h3>Профил</h3>
		</Link>
        <Link id = "menuButtons" to="/" onClick={() => localStorage.removeItem("token")}>
        <LogoutIcon fontSize='large'></LogoutIcon>
        <h3>Изход</h3>
		</Link>
        </div>
	</div>
    </div>
  )
}

export default MenuEM