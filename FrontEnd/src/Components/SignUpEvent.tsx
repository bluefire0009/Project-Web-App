import React, { useState } from 'react';
import { CalendarEvent } from '../States/CalendarState';
import Api_url from './Api_url';
interface SignUpEventProps {
    event: CalendarEvent;
    currentUserId: string;
}

const SignUpEvent: React.FC<SignUpEventProps> = ({ event, currentUserId }) => {
    const [isSignedUp, setIsSignedUp] = useState(false);

    const handleSignUp = async () => {
        try {
            const response = await fetch(`${Api_url}/api/attendance`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    userId: currentUserId,
                    //eventId: event.EventId,
                    attendanceDate: event.StartTime,
                }),
            });

            if (response.ok) {
                setIsSignedUp(true);
                console.log('Signed up successfully');
            } else {
                console.error('Error signing up:', response.statusText);
            }
        } catch (error) {
            console.error('Error signing up:', error);
        }
    };

    return (
        <div>
            <h3>{event.Title}</h3>
            <p>{event.Description}</p>
            <button
                onClick={handleSignUp}
                disabled={isSignedUp}
                style={{
                    padding: '10px 20px',
                    backgroundColor: isSignedUp ? 'gray' : '#007BFF',
                    color: 'white',
                    border: 'none',
                    borderRadius: '5px',
                    cursor: isSignedUp ? 'not-allowed' : 'pointer',
                }}
            >
                {isSignedUp ? 'Signed Up' : 'Sign Up'}
            </button>
        </div>
    );
};

export default SignUpEvent;