import React from 'react';
import Reviews from './Components/Reviews';
import UserPageMain from './Components/UserPageMain';
import SelectedDay from './Components/SelectedDay'
import { BrowserRouter, Routes, Route } from "react-router";
import Navbar from './Components/Navbar';
import Footer from './Components/Footer';
import './App.css';
import "./Styling/MonthCalendar.css";
import "./Styling/EventSignupOverlay.css";
import "./Styling/WeekCalendar.css"
import "./Styling/Footer.css"
import "./Styling/CalendarPage.css"
import { CalendarPage } from './Components/CalendarPage';
import { SignInForm } from './Components/loginPage';
import RegistrationForm from './Components/RegistrationPage';

const App: React.FC = () => {
  return (
    <div className="main">
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
          <Route path="/Login" element={<SignInForm />} />
        </Routes>
        <Footer/>
      </BrowserRouter>
    </div>
  );
};

export default App;