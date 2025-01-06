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

const App: React.FC = () => {
  return (
    <div style={{ margin: '0 20px', gridTemplateRows: 'auto 1fr auto', display: 'grid', minHeight: '100vh'}}>
      <BrowserRouter>
        <Navbar/>
        <Routes>
          {/* homepage */}
          <Route path="/" element={<SelectedDay />} />
          
          <Route path="/user" element={
            <UserPageMain
            UserName='Micheal "toast" Grobsker'
            workSchedules={[{title: 'work', place: 'office', date:'45-8-1997', time: '11:3½', description: 'workTime at the office'}]}
            eventSchedules={[{title: 'Party', place: 'office', date:'45-8-1997', time: '11:3½', 
            description: 'party time at the office, Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt turpis sed mi placerat, semper fringilla eros elementum. Morbi id dolor suscipit, elementum quam quis, eleifend augue. Nunc egestas orci non purus tincidunt, non vulputate nunc ultricies. Integer in tempus neque. Nam at lectus ex. Sed finibus magna sed ornare pharetra posuere. '}]}/>
          } />
          <Route path="/day" element={<SelectedDay />} />
          <Route path="/event" element={<SelectedEvent />} />
          <Route path="/reviews" element={<Reviews />} />
          <Route path="/signup" element={<SignUpSection />} />
        </Routes>
        <Footer/>
      </BrowserRouter>
    </div>
    
  );
};

export default App;