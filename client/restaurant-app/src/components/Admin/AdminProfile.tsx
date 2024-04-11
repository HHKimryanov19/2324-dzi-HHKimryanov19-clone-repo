import '../../style/profileStyle.css'
import ProfileMP from "../Profile"
import MenuAdmin from '../Menus/MenuAdmin'

function ProfileAdmin() {
    return (
      <div id="page">
        <MenuAdmin></MenuAdmin>
        <ProfileMP></ProfileMP>
      </div>
    )
}

export default ProfileAdmin