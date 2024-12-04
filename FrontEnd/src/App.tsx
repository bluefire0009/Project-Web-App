import React from 'react';
import SelectedEvent from './components/SelectedEvent';
import Reviews from './components/Reviews';
import SignUpSection from './components/SignUpSection';
import UserPageMain from './components/UserPageMain';
import './App.css';

const App: React.FC = () => {
  return (
    <div style={{ margin: '20px' }}>
      <SelectedEvent />
      <Reviews />
      <SignUpSection />

      <UserPageMain
        UserName='Micheal "toast" Grobsker'
        workSchedules={[{title: 'work', place: 'office', date:'45-8-1997', time: '11:3½', description: 'workTime at the office'}]}
        eventSchedules={[{title: 'Party', place: 'office', date:'45-8-1997', time: '11:3½', description: 'party time at the office, Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt turpis sed mi placerat, semper fringilla eros elementum. Morbi id dolor suscipit, elementum quam quis, eleifend augue. Nunc egestas orci non purus tincidunt, non vulputate nunc ultricies. Integer in tempus neque. Nam at lectus ex. Sed finibus magna sed ornare pharetra posuere. '}]}>
      </UserPageMain>
    </div>
    
  );
};

export default App;