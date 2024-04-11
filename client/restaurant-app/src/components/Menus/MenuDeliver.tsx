import HomeIcon from '@mui/icons-material/Home';
import PersonIcon from '@mui/icons-material/Person';
import LogoutIcon from '@mui/icons-material/Logout';
import '../../style/Menu1.css'
import { Link } from 'react-router-dom' 

function MenuDeliver() {
  return (
    <div id='componentBody'>
    <div id="mainDiv">
		<div id="secondDiv">
			<Link id = "menuButtons" to="/deliver-page">
            <HomeIcon fontSize='large'></HomeIcon>
            <h3>Начало</h3>
			</Link>
		</div>
        <div id="thirdDiv">
        <Link id = "menuButtons" to="/deliver/profile">
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

export default MenuDeliver