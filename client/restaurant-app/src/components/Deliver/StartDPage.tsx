import Orders from "./Orders"
import MenuDeliver from "../Menus/MenuDeliver"

function StartDPage()
{
    return (
        <div id="page">
            <MenuDeliver></MenuDeliver>
            <Orders></Orders>
        </div>
    )
}

export default StartDPage