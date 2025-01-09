import React, { useState, useEffect } from 'react';
import { AdminPageStateProvider, useAdminPageState } from '../States/AdminPageState';
import '../Styling/AdminPage.css';

const AdminDashboard: React.FC = () => {
    const { showCreateEventForm, setShowCreateEventForm, showEventList, setShowEventList } = useAdminPageState();
    const [events, setEvents] = useState<any[]>([]);
    const [selectedEvent, setSelectedEvent] = useState<string | null>(null);
    const [editingReviewId, setEditingReviewId] = useState<string | null>(null);
    const [reviewContent, setReviewContent] = useState<string>('');
    const dummyReviews = [
        { id: '1', name: 'Stars', rating: 5, content: 'Great event!' },
        { id: '2', name: 'Sun', rating: 4, content: 'Had a good time.' },
    ];

    useEffect(() => {
        fetchEvents();
    }, []);

    const fetchEvents = async () => {
        try {
            const response = await fetch('http://localhost:5097/api/events');
            const data = await response.json();
            console.log('Fetched events:', data); 
            setEvents(data);
        } catch (error) {
            console.error('Error fetching events:', error);
        }
    };

    const handleCreateEventClick = () => {
        setShowCreateEventForm(true);
        setShowEventList(false);
    };

    const handleShowEventListClick = () => {
        setShowEventList(true);
        setShowCreateEventForm(false);
    };

    const handleBackToDashboardClick = () => {
        setShowCreateEventForm(false);
        setShowEventList(false);
        setSelectedEvent(null);
        setEditingReviewId(null);
    };

    const handleSelectEvent = (eventId: string) => {
        setSelectedEvent(eventId);
    };

    const handleEditReviewClick = (reviewId: string, content: string) => {
        setEditingReviewId(reviewId);
        setReviewContent(content);
    };

    const handleReviewContentChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setReviewContent(e.target.value);
    };

    const handleSaveReviewClick = () => {
        const reviewIndex = dummyReviews.findIndex(review => review.id === editingReviewId);
        if (reviewIndex !== -1) {
            dummyReviews[reviewIndex].content = reviewContent;
        }
        setEditingReviewId(null);
    };

    const handleCreateEvent = async (e: React.FormEvent) => {
        e.preventDefault();
        const newEvent = {
            name: (e.target as any).eventName.value,
            date: (e.target as any).eventDate.value,
            description: (e.target as any).eventDescription.value,
        };
        try {
            const response = await fetch('http://localhost:5097/api/events', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newEvent),
            });
            if (response.ok) {
                fetchEvents();
                setShowCreateEventForm(false);
            } else {
                console.error('Error creating event:', response.statusText);
            }
        } catch (error) {
            console.error('Error creating event:', error);
        }
    };

    return (
        <div className="dashboard">
            <h1 className="title">Admin Dashboard</h1>
            <div className="container">
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
                        <form onSubmit={handleCreateEvent}>
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
                        {(
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
                        {dummyReviews.map(review => (
                            <div key={review.id} className="review">
                                <p><strong>{review.name}</strong></p>
                                <p>Rating: {review.rating} / 5</p>
                                {editingReviewId === review.id ? (
                                    <>
                                        <textarea
                                            value={reviewContent}
                                            onChange={handleReviewContentChange}
                                        />
                                        <button onClick={handleSaveReviewClick}>Save</button>
                                    </>
                                ) : (
                                    <>
                                        <p>{review.content}</p>
                                        <button onClick={() => handleEditReviewClick(review.id, review.content)}>Edit</button>
                                    </>
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