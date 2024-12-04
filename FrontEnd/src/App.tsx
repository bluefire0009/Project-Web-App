import React from 'react';
import SelectedEvent from './components/SelectedEvent';
import Reviews from './components/Reviews';
import SignUpSection from './components/SignUpSection';
import './App.css';

const App: React.FC = () => {
  return (
    <div style={{ margin: '20px' }}>
      <SelectedEvent />
      <Reviews />
      <SignUpSection />
    </div>
  );
};

export default App;