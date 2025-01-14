import React from 'react';
import SignUpEvent from './SignUpEvent';
import { CalendarEvent } from '../States/CalendarState';

const SignUpSection: React.FC<{ event: CalendarEvent, currentUserId: string }> = ({ event, currentUserId }) => {
  return (
    <div>
      {/* Container for Sign Up and Remove buttons */}
      <div style={{ display: 'flex', justifyContent: 'center', gap: '20px'}}>
        {/* Sign Up button section */}
        <div style={{ textAlign: 'center' }}>
          <h3 style={{ padding: '5px', margin: '0px'}}>Sign up</h3>
          <p style={{marginBottom: '10px'}}>Sign up for event</p>
          <SignUpEvent event={event} currentUserId={currentUserId} />
        </div>
        {/* Remove Sign Up button section */}
        <div style={{ textAlign: 'center' }}>
          <h3 style={{ padding: '5px', margin: '0px'}}>Remove sign up</h3>
          <p style={{marginBottom: '10px'}}>Remove your signup from the event</p>
          <button style={{ padding: '10px 20px', backgroundColor: '#007BFF', color: 'white', border: 'none', borderRadius: '5px', cursor: 'pointer' }}>
            REMOVE
          </button>
        </div>
      </div>
    </div>
  );
};

export default SignUpSection;