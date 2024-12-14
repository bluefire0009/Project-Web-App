import React from 'react';
import SelectedEvent from './Components/SelectedEvent';
import Reviews from './Components/Reviews';
import SignUpSection from './Components/SignUpSection';
import UserPageMain from './Components/UserPageMain';
import SelectedDay from './Components/SelectedDay'
import './App.css';
import SignInForm from './Components/loginPage';

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
      <SelectedDay />
      <SignInForm />
    </div>
    
  );
};

export default App;