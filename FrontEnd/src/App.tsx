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
    <div style={{ margin: '20px' }}>
      <Reviews />
      <SignUpSection />
      <UserPageMain UserId={4}/>
      <SelectedDay />
      {/* <CalendarPage/> */}
    </div>
  );
};

export default App;