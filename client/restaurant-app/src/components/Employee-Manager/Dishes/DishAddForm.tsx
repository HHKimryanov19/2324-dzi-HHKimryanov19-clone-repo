import DishIM from '../../../models/InputModels/DishIM';
import DishService from '../../../services/dish';
import '../../../style/bookingStyle/bookingFormStyle.css'
import { useState } from 'react'
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import '../../../style/employeeStyle.css'

function DishAddForm() {
    const [file, setFile] = useState<Blob>()
    const [dish, setDish] = useState<DishIM>({
        title: '',
        info: '',
        price: 0,
        category: 1
    })

    function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
      const { name, value } = event.target;
      if(name === 'category') {
        setDish({ ...dish, category: Number(value) });
      }
      else
      {
        setDish({ ...dish, [name]: value });
      }
    }
  
    async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
      event.preventDefault();
      var ds = new DishService()
      ds.addDish(dish, file).then(() => {})
    }

    return (
      <>
      <form method="POST" onSubmit={handleSubmit} encType="multipart/form-data">
        <div>
        <input className='updateInput' type='file' name='image' onChange={(event) => {
          if(event.target.files !== null)
          {
            setFile(new Blob([event.target.files[0]], {type: 'image/jpg'}))
          }
        }}/>
        </div>
        <input className='updateInput' type="text" name='title' value={dish.title} onChange={handleInputChange} placeholder='Наименование' required/>
        <input className='updateInput' type="text" name='info' value={dish.info} onChange={handleInputChange} placeholder='Информация' required/>
        <input className='updateInput' type='text' name='price' value={dish.price} onChange={handleInputChange} placeholder='Цена' required/>
        <FormControl id="select" fullWidth>
            <InputLabel id="demo-simple-select-label">Категория</InputLabel>
              <Select
               value={dish.category}
               label="Restaurant"
               onChange={(event) => setDish({...dish, category:Number(event.target.value)})}>
                <MenuItem value={1}>Морски</MenuItem>
                <MenuItem value={2}>Месо</MenuItem>
                <MenuItem value={3}>Вегетарианаски</MenuItem>
                <MenuItem value={4}>Десерт</MenuItem>
                <MenuItem value={5}>Пица</MenuItem>
                <MenuItem value={6}>Салата</MenuItem>
              </Select>
          </FormControl>
        <button type="submit" id='formButton'>Добави ново</button>
      </form>
      </>
    )
}

export default DishAddForm