import '../style/App.css'
import { Link} from 'react-router-dom'
import '../style/commonStyle/common.css'

function App() {
    return (
      <>
      <div id="componentBodyApp">
        <h1>Добре дошли в системата за поръчки</h1>
        <div id = "startBtn">
          <Link className="btn" to="/register">Регистрация</Link>
          <Link className="btn" to="/login">Вход</Link>
        </div>
      </div>
      </>
    )
}

export default App
