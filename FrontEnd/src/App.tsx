import React from 'react';
import SelectedEvent from './Components/SelectedEvent';
import Reviews from './Components/Reviews';
import SignUpSection from './Components/SignUpSection';
import UserPageMain from './Components/UserPageMain';
import SelectedDay from './Components/SelectedDay'
import './App.css';
import { MonthCalendar } from './Components/MonthCalendar';
import "./Styling/MonthCalendar.css";
import "./Styling/EventSignupOverlay.css";
import "./Styling/WeekCalendar.css"
import "./Styling/Footer.css"
import "./Styling/CalendarPage.css"
import { WeekCalendar } from './Components/WeekCalendar';
import { CalendarPage } from './Components/CalendarPage';

const App: React.FC = () => {
  return (
    <div>
      {/* <UserPageMain
        UserName='Micheal "toast" Grobsker'
        workSchedules={[{title: 'work', place: 'office', date:'45-8-1997', time: '11:3½', description: 'workTime at the office'}]}
        eventSchedules={[{title: 'Party', place: 'office', date:'45-8-1997', time: '11:3½', description: 'party time at the office, Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt turpis sed mi placerat, semper fringilla eros elementum. Morbi id dolor suscipit, elementum quam quis, eleifend augue. Nunc egestas orci non purus tincidunt, non vulputate nunc ultricies. Integer in tempus neque. Nam at lectus ex. Sed finibus magna sed ornare pharetra posuere. '}]}>
      </UserPageMain>
      <SelectedDay />
      <WeekCalendar/>
      <MonthCalendar/>
       */}
      
      <CalendarPage/>
      
    </div>
    
  );
};

export default App;