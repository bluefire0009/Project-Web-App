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
import "./Styling/MonthCalendar.css";
import "./Styling/EventSignupOverlay.css";
import "./Styling/WeekCalendar.css"
import "./Styling/Footer.css"
import "./Styling/CalendarPage.css"
import { CalendarPage } from './Components/CalendarPage';
import { SignInForm } from './Components/loginPage';
import RegistrationForm from './Components/RegistrationPage';
import ContactScreen from './Components/contact';

import AdminDashboard from './Components/AdminPage';
import { ListAllUsers } from './Components/ListAllUsers';
const App: React.FC = () => {
  return (
    <div style={{ margin: '0 20px', gridTemplateRows: 'auto 1fr auto', display: 'grid', minHeight: '100vh' }}>
      <BrowserRouter>
        <Navbar />
        <div className='MainContent'>
          <Routes>
            {/* homepage */}
            <Route path="/" element={<SelectedDay />} />

            <Route path="/user/:UserId?" element={<UserPageMain />} />
            <Route path="/day" element={<SelectedDay />} />
            <Route path='/calendar' element={<CalendarPage />} />
            <Route path="/reviews" element={<Reviews />} />
            <Route path="/signup" element={<SignUpSection />} />
            <Route path="/Login" element={<SignInForm />} />
            <Route path="/Register" element={<RegistrationForm />} />
            <Route path="/contact" element={<ContactScreen />} />
            <Route path="/admin" element={<AdminDashboard />} />
            <Route path="/AllUsers" element={<ListAllUsers />} />
          </Routes>
        </div>
        <Footer />
      </BrowserRouter>
    </div>
  );
};

export default App;