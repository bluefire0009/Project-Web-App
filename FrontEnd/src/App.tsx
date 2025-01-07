import React from 'react';
import SelectedEvent from './Components/SelectedEvent';
import Reviews from './Components/Reviews';
import SignUpSection from './Components/SignUpSection';
import UserPageMain from './Components/UserPageMain';
import SelectedDay from './Components/SelectedDay'
import './App.css';

const App: React.FC = () => {
  return (
    <div style={{ margin: '20px' }}>
      <SelectedEvent />
      <Reviews />
      <SignUpSection />

      <UserPageMain UserId={4}/>
      <SelectedDay />
    </div>
    
  );
};

export default App;