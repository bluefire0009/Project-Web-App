import React, { useState, useEffect } from 'react';
import Api_url from './Api_url';
import { CalendarEvent } from '../States/CalendarState';
import { format } from 'date-fns';

const SignUpEvent: React.FC<{ event: CalendarEvent; currentUserId: number }> = ({ event, currentUserId }) => {
    const [isSignedUp, setIsSignedUp] = useState(false);
    const [message, setMessage] = useState('');
    const [eventAttendanceId, setEventAttendanceId] = useState<bigint | null>(null);

    useEffect(() => {
        console.log('Event:', event);
        checkIfSignedUp();
    }, [event]);
    //Check if the user is already signed up for the event
    const checkIfSignedUp = async () => {
        if (!event) return;

        try {
            const response = await fetch(`${Api_url}/api/eventAttendance/GetAll`);
            if (response.ok) {
                const result = await response.json();
                console.log('API Response:', result);

                const validAttendances = result.filter(
                    (attendance: any) =>
                        attendance.eventId !== undefined &&
                        attendance.userId !== undefined
                );

                const userAttendance = validAttendances.find((attendance: any) => {
                    console.log(
                        'Comparing:',
                        BigInt(attendance.eventId),
                        event.EventId,
                        BigInt(attendance.eventId) === event.EventId
                    );
                    return (
                        attendance.userId === currentUserId &&
                        BigInt(attendance.eventId) === event.EventId
                    );
                });

                console.log('User Attendance:', userAttendance);

                if (userAttendance) {
                    setIsSignedUp(true);
                    setEventAttendanceId(userAttendance.event_AttendanceId);
                } else {
                    setIsSignedUp(false);
                    setEventAttendanceId(null);
                }
            } else {
                console.error('Failed to fetch attendance events');
            }
        } catch (error) {
            console.error('Error checking sign-up status:', error);
        }
    };
   //Sign up user for event
    const handleSignUp = async () => {
        if (!event) return;

        const attendanceDate = format(event.StartTime, 'yyyy-MM-dd');

        try {
            const response = await fetch(`${Api_url}/api/eventAttendance/Post`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Rating: "", 
                    Feedback: "", 
                    UserId: currentUserId,
                    EventId: Number(event.EventId),
                    DatePlaced: attendanceDate 
                }),
            });

            if (response.ok) {
                const result = await response.json();
                setIsSignedUp(true);
                setEventAttendanceId(result.Event_AttendanceId);
                setMessage(result.message);
                console.log('Signed up successfully');
            } else {
                const errorText = await response.text();
                setMessage(`Failed to sign up: ${errorText}`);
                console.log('Failed to sign up:', errorText);
            }
        } catch (error) {
            setMessage(`Error signing up: ${error instanceof Error ? error.message : 'Unknown error'}`);
            console.error('Error signing up:', error);
        }
    };

    // Delete user from event
    const handleSignOff = async () => {
    if (!event || eventAttendanceId === null) return;

    try {
        
        const response = await fetch(`${Api_url}/api/eventAttendance/Delete?id=${eventAttendanceId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        
        if (response.ok) {
            const contentType = response.headers.get('Content-Type');

            if (contentType && contentType.includes('application/json')) {
                
                const result = await response.json();
                setIsSignedUp(false); 
                setEventAttendanceId(null); 
                setMessage(result.message || 'Successfully signed off.');
                console.log('Signed off successfully:', result);
            } else {
                
                const resultText = await response.text();
                setMessage(`Error: ${resultText}`);
                console.log('Error signing off (non-JSON response):', resultText);
            }
        } else {
            
            const errorText = await response.text();
            setMessage(`Failed to sign off: ${errorText}`);
            console.log('Failed to sign off (error response):', errorText);
        }
    } catch (error) {
        // Catch any network or unexpected errors
        setMessage(`Error signing off: ${error instanceof Error ? error.message : 'Unknown error'}`);
        console.error('Error signing off:', error);
    }
};

    const isValidDate = (date: any) => {
        return date instanceof Date && !isNaN(date as any);
    };

    if (!event) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            {isValidDate(event.StartTime) && isValidDate(event.EndTime) ? (
                <div>
                    <div>Here you can see more information about the selected event.</div>
                    <table>
                        <thead>
                            <tr>
                                <th>Events</th>
                                <th>Place</th>
                                <th>Date</th>
                                <th>Time</th>
                                <th>Description</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>{event.Title}</td>
                                <td>{event.Location}</td>
                                <td>{format(event.StartTime, 'dd/MM/yyyy')}</td>
                                <td>{format(event.StartTime, "kk:mm")} - {format(event.EndTime, "kk:mm")}</td>
                                <td>{event.Description}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            ) : (
                <div>Invalid event date</div>
            )}
            <button onClick={handleSignUp} disabled={isSignedUp}>
                {isSignedUp ? 'Signed Up' : 'Sign Up'}
            </button>
            <button onClick={handleSignOff} disabled={!isSignedUp}>
                {isSignedUp ? 'Sign Off' : 'Not Signed Up'}
            </button>
            {message && <div>{message}</div>}
        </div>
    );
};

export default SignUpEvent;