import React, { useState, useEffect } from 'react';
import { AdminPageStateProvider, useAdminPageState } from '../States/AdminPageState';
import '../Styling/AdminPage.css';
import Api_url from './Api_url';

const AdminDashboard: React.FC = () => {
    const { showCreateEventForm, setShowCreateEventForm, showEventList, setShowEventList } = useAdminPageState();
    const [events, setEvents] = useState<any[]>([]);
    const [selectedEvent, setSelectedEvent] = useState<string | null>(null);
    const [editingReviewId, setEditingReviewId] = useState<string | null>(null);
    const [reviewContent, setReviewContent] = useState<string>('');
    const [feedbackMessage, setFeedbackMessage] = useState<string | null>(null);
    const [eventReviews, setEventReviews] = useState<any[]>([]);
    const dummyReviews = [
        { id: '1', name: 'Peter Parker', rating: 5, content: 'Great event!' },
        { id: '2', name: 'Miles Morales', rating: 4, content: 'Had a good time.' },
        { id: '3', name: 'Gwen Stacy', rating: 3, content: 'It was okay, but could have been better.' }
    ];

    useEffect(() => {
        getAllEvents();
    }, []);

    const getAllEvents = async () => {
        try {
            const response = await fetch(`${Api_url}/api/events`, {
                method: 'GET',
                credentials: 'include',
            });

            if (response.ok) {
                const data = await response.json();
                console.log('Fetched events:', data); // Debugging step
                setEvents(data);
            } else {
                console.error('Error fetching events:', response.statusText);
            }
        } catch (error) {
            console.error('Error fetching events:', error);
        }
    };

    const handleCreateEventClick = () => {
        setShowCreateEventForm(true);
        setShowEventList(false);
        setFeedbackMessage(null); // Clear any previous feedback message
    };
    
    const handleShowEventListClick = () => {
        setShowEventList(true);
        setShowCreateEventForm(false);
        setFeedbackMessage(null); // Clear any previous feedback message
    };
    
    const handleBackToDashboardClick = () => {
        setShowCreateEventForm(false);
        setShowEventList(false);
        setSelectedEvent(null);
        setEditingReviewId(null);
        setFeedbackMessage(null); // Clear any previous feedback message
    };
    
    const handleSelectEvent = (eventId: string) => {
        setSelectedEvent(eventId);
        // Show the 3 dummy reviews directly
        setEventReviews(dummyReviews);
    };
    
    const handleEditReviewClick = (reviewId: string, content: string) => {
        setEditingReviewId(reviewId);
        setReviewContent(content);
    };
    const handleReviewContentChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setReviewContent(e.target.value);
    };

    const handleSaveReviewClick = () => {
        const updatedReviews = eventReviews.map(review => 
            review.id === editingReviewId ? { ...review, content: reviewContent } : review
        );
        setEventReviews(updatedReviews);
        setEditingReviewId(null);
        setReviewContent('');
    };

    const handleCreateEvent = async (e: React.FormEvent) => {
        e.preventDefault();
        const newEvent = {
            title: (e.target as any).eventName.value,
            description: (e.target as any).eventDescription.value,
            eventDate: (e.target as any).eventDate.value,
            startTime: "00:00:00", // Add default values if necessary
            endTime: "23:59:59",   // Add default values if necessary
            location: "Default Location", // Add default values if necessary
            adminApproval: false // Default value for admin approval
        };
        try {
            const response = await fetch(`${Api_url}/api/events/create`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newEvent),
            });
            if (response.ok) {
                getAllEvents();
                setShowCreateEventForm(false);
                setFeedbackMessage('Event created successfully!');
            } else {
                console.error('Error creating event:', response.statusText);
                setFeedbackMessage('Failed to create event.');
            }
        } catch (error) {
            console.error('Error creating event:', error);
            setFeedbackMessage('Failed to create event.');
        }
    };

    return (
        <div className="dashboard">
            <h1 className="title">Admin Dashboard</h1>
            <div className="container">
                {feedbackMessage && <p className="feedback">{feedbackMessage}</p>}
                {!showCreateEventForm && !showEventList ? (
                    <>
                        <div className="section">
                            <h2>Create Event</h2>
                            <button onClick={handleCreateEventClick}>Create Event</button>
                        </div>
                        <div className="section">
                            <h2>Manage Events</h2>
                            <button onClick={handleShowEventListClick}>Show Event List</button>
                        </div>
                    </>
                ) : showCreateEventForm ? (
                    <div className="section">
                        <h2>Create Event</h2>
                        <form className="form" onSubmit={handleCreateEvent}>
                            <div className="formGroup">
                                <label htmlFor="eventName">Event Name:</label>
                                <input type="text" id="eventName" name="eventName" />
                            </div>
                            <div className="formGroup">
                                <label htmlFor="eventDate">Event Date:</label>
                                <input type="date" id="eventDate" name="eventDate" />
                            </div>
                            <div className="formGroup">
                                <label htmlFor="eventDescription">Event Description:</label>
                                <textarea id="eventDescription" name="eventDescription"></textarea>
                            </div>
                            <button type="submit">Create Event</button>
                        </form>
                        <button onClick={handleBackToDashboardClick}>Back to Dashboard</button>
                    </div>
                ) : showEventList ? (
                    <div className="section">
                        <h2>Event List</h2>
                        {events.length === 0 ? (
                            <p>No events available</p>
                        ) : (
                            events.map(event => (
                                <div key={event.eventId} className="event" onClick={() => handleSelectEvent(event.eventId)}>
                                    {event.title}
                                </div>
                            ))
                        )}
                        <button onClick={handleBackToDashboardClick}>Back to Dashboard</button>
                    </div>
                ) : null}
                {selectedEvent && (
                    <div className="reviews">
                        <h3>Recent Reviews</h3>
                        {eventReviews.map(review => (
                            <div key={review.id} className="review">
                                <p><strong>{review.name}</strong></p>
                                <p>Rating: {review.rating}</p>
                                <p>{review.content}</p>
                                {editingReviewId === review.id ? (
                                    <>
                                        <textarea
                                            value={reviewContent}
                                            onChange={handleReviewContentChange}
                                        />
                                        <button onClick={handleSaveReviewClick}>Save</button>
                                    </>
                                ) : (
                                    <button onClick={() => handleEditReviewClick(review.id, review.content)}>Edit</button>
                                )}
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </div>
    );
};

const AdminPage: React.FC = () => {
    return (
        <AdminPageStateProvider>
            <AdminDashboard />
        </AdminPageStateProvider>
    );
};

export default AdminPage;