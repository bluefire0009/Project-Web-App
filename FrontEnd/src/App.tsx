import React, { useEffect, useState } from 'react';
import SelectedEvent from './Components/SelectedEvent';
import Reviews from './Components/Reviews';
import SignUpSection from './Components/SignUpSection';
import UserPageMain from './Components/UserPageMain';
import SelectedDay from './Components/SelectedDay'
// import { BrowserRouter, Routes, Route } from "react-router";
import { BrowserRouter, Routes, Route } from "react-router-dom";
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
import PrivateRoute from './Components/ProtectedRoute';
import { AdminLoggedIn, UserLoggedIn } from './Components/LoginChecker';
const App: React.FC = () => {

  const [isUserLoggedIn, setIsUserLoggedIn] = useState<boolean>(false);
  const [isAdminLoggedIn, setIsAdminLoggedIn] = useState<boolean>(false);

  useEffect(() => {
    // Check if the user or admin is logged in
    const checkAuthStatus = async () => {
      const userLoggedIn = await UserLoggedIn();
      const adminLoggedIn = await AdminLoggedIn();

      setIsUserLoggedIn(userLoggedIn);
      setIsAdminLoggedIn(adminLoggedIn);
    };

    checkAuthStatus();
  }, []);

  return (
    <div style={{ margin: '0 20px', gridTemplateRows: 'auto 1fr auto', display: 'grid', minHeight: '100vh' }}>
      <BrowserRouter>
        <Navbar />
        <div className='MainContent'>
          <Routes>
            {/* Public route */}
            <Route path="/" element={<SignInForm />} />

            {/* Protected routes */}
            <Route
              path="/user/:UserId?"
              element={<PrivateRoute isAuthenticated={isUserLoggedIn} element={<UserPageMain />} />}
            />
            <Route
              path="/day"
              element={<PrivateRoute isAuthenticated={isUserLoggedIn || isAdminLoggedIn} element={<SelectedDay />} />}
            />
            <Route
              path="/calendar"
              element={<PrivateRoute isAuthenticated={isUserLoggedIn || isAdminLoggedIn} element={<CalendarPage />} />}
            />
            <Route
              path="/signup"
              element={<PrivateRoute isAuthenticated={isUserLoggedIn || isAdminLoggedIn} element={<SignUpSection />} />}
            />
            <Route
              path="/login"
              element={<SignInForm />}  // Login can be public, no need for PrivateRoute
            />
            <Route
              path="/Register"
              element={<RegistrationForm />}
            />
            <Route
              path="/contact"
              element={<ContactScreen />}
            />
            <Route
              path="/admin"
              element={<PrivateRoute isAuthenticated={isAdminLoggedIn} element={<AdminDashboard />} />}
            />
            <Route
              path="/AllUsers"
              element={<PrivateRoute isAuthenticated={isAdminLoggedIn} element={<ListAllUsers />} />}
            />
          </Routes>
        </div>
        <Footer />
      </BrowserRouter>
    </div>
  );
};

export default App;