import '../../style/profileStyle.css'
import ProfileMP from "../Profile"
import MenuDeliver from '../Menus/MenuDeliver'

function ProfileDeliver() {
    return (
      <div id="page">
        <MenuDeliver></MenuDeliver>
        <ProfileMP></ProfileMP>
      </div>
    )
}

export default ProfileDeliver