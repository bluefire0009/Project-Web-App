import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Events from './pages/events/Events';


const AppRoutes = () => (
  <Router>
    <Routes>
      <Route path="/" element={<Events />} />
     
    </Routes>
  </Router>
);

export default AppRoutes;