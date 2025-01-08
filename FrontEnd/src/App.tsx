import React from 'react';
import SelectedEvent from './Components/SelectedEvent';
import Reviews from './Components/Reviews';
import SignUpSection from './Components/SignUpSection';
import UserPageMain from './Components/UserPageMain';
import SelectedDay from './Components/SelectedDay'
import { BrowserRouter, Routes, Route } from "react-router";
import Navbar from './Components/Navbar';
import Footer from './Components/Footer';
import './App.css';
import { MonthCalendar } from './Components/MonthCalendar';
import "./Styling/MonthCalendar.css";
import "./Styling/EventSignupOverlay.css";
import "./Styling/WeekCalendar.css"
import "./Styling/Footer.css"
import "./Styling/CalendarPage.css"
import { WeekCalendar } from './Components/WeekCalendar';
import { CalendarPage } from './Components/CalendarPage';

import AdminDashboard from './Components/AdminPage';
const App: React.FC = () => {
  return (
    <div style={{ margin: '0 20px', gridTemplateRows: 'auto 1fr auto', display: 'grid', minHeight: '100vh'}}>
      <BrowserRouter>
        <Navbar/>
        <Routes>
          {/* homepage */}
          <Route path="/" element={<SelectedDay />} />
          
          <Route path="/user" element={<UserPageMain UserId={4}/>} />
          <Route path="/day" element={<SelectedDay />} />
          <Route path='/calendar' element={<CalendarPage/>}/>
          <Route path="/reviews" element={<Reviews />} />
          <Route path="/signup" element={<SignUpSection />} />
          <Route path="/admin" element={<AdminDashboard />} />
        </Routes>
        <Footer/>
      </BrowserRouter>
    </div>
  );
};

export default App;