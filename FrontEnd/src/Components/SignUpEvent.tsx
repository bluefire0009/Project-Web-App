import React, { useState, useEffect } from 'react';
import { CalendarEvent } from '../States/CalendarState';
import Api_url from './Api_url';

interface SignUpEventProps {
    event: CalendarEvent;
    currentUserId: string;
}

const SignUpEvent: React.FC<SignUpEventProps> = ({ event, currentUserId }) => {
    const [isSignedUp, setIsSignedUp] = useState(false);

    useEffect(() => {
        console.log('Event:', event);
    }, [event]);

    const handleSignUp = async () => {
        try {
            const response = await fetch(`${Api_url}/api/attendance/Add`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    userId: currentUserId,
                    //eventId: event.EventId,
                    //attendanceDate: event.date,
                }),
            });

            if (response.ok) {
                setIsSignedUp(true);
                console.log('Signed up successfully');
            } else {
                console.log('Failed to sign up');
            }
        } catch (error) {
            console.error('Error signing up:', error);
        }
    };

    return (
        <div>
            <button onClick={handleSignUp} disabled={isSignedUp}>
                {isSignedUp ? 'Signed Up' : 'Sign Up'}
            </button>
        </div>
    );
};

export default SignUpEvent;