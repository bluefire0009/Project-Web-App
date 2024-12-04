import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import UserPageMain from './Components/UserPageMain.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <UserPageMain
      UserName='Micheal "toast" Grobsker'
      workSchedules={[{title: 'work', place: 'office', date:'45-8-1997', time: '11:3½', description: 'workTime at the office'}]}
      eventSchedules={[{title: 'Party', place: 'office', date:'45-8-1997', time: '11:3½', description: 'party time at the office, Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt turpis sed mi placerat, semper fringilla eros elementum. Morbi id dolor suscipit, elementum quam quis, eleifend augue. Nunc egestas orci non purus tincidunt, non vulputate nunc ultricies. Integer in tempus neque. Nam at lectus ex. Sed finibus magna sed ornare pharetra posuere. '}]}>
    </UserPageMain>
  </StrictMode>,
)
