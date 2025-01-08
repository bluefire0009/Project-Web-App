import React, { useState } from 'react';
import { AdminPageStateProvider, useAdminPageState } from '../States/AdminPageState';
import '../Styling/AdminPage.css';

const AdminDashboard: React.FC = () => {
    const { showCreateEventForm, setShowCreateEventForm, showEventList, setShowEventList } = useAdminPageState();
    const [selectedEvent, setSelectedEvent] = useState<string | null>(null);
    const [editingReviewId, setEditingReviewId] = useState<string | null>(null);
    const [reviewContent, setReviewContent] = useState<string>('');

    const dummyEvent = { id: '1', name: 'Sample Event' };
    const dummyReviews = [
        { id: '1', name: 'Peter Parker', rating: 5, content: 'Great event!' },
        { id: '2', name: 'Miles Morales', rating: 4, content: 'Had a wonderful time.' },
        { id: '3', name: 'Miguel O Hara', rating: 3, content: 'Looking forward to the next one.' },
    ];

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
                        <form>
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
                ) : (
                    <div className="section">
                        <h2>Event List</h2>
                        <div className="event" onClick={() => handleSelectEvent(dummyEvent.id)}>
                            {dummyEvent.name}
                        </div>
                        <button onClick={handleBackToDashboardClick}>Back to Dashboard</button>
                    </div>
                )}
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