import React, { useState } from "react";
import "./Events.css";

function App() {
  // States to manage events, filter and modal visibility
  const [events, setEvents] = useState([
    { id: 1, name: "Event 1", place: "Location 1", date: "2024-12-12", time: "10:00 AM", description: "Event 1 description", signedUp: false, review: "", rating: 0 },
    { id: 2, name: "Event 2", place: "Location 2", date: "2024-12-15", time: "2:00 PM", description: "Event 2 description", signedUp: false, review: "", rating: 0 },
    { id: 3, name: "Event 3", place: "Location 3", date: "2024-12-18", time: "5:00 PM", description: "Event 3 description", signedUp: false, review: "", rating: 0 },
  ]);
  
  const [filter, setFilter] = useState("all");
  const [modalVisible, setModalVisible] = useState(false);
  const [currentEventId, setCurrentEventId] = useState(null);
  const [reviewText, setReviewText] = useState("");
  const [rating, setRating] = useState(0);

  // Handle signup, remove signup, and review
  const handleSignUp = (id) => {
    setEvents(events.map(event => event.id === id ? { ...event, signedUp: true } : event));
  };

  const handleRemoveSignUp = (id) => {
    setEvents(events.map(event => event.id === id ? { ...event, signedUp: false, review: "", rating: 0 } : event));
  };

  const handleReview = (id) => {
    setModalVisible(true);
    setCurrentEventId(id);
  };

  const submitReview = () => {
    setEvents(events.map(event => 
      event.id === currentEventId 
      ? { ...event, review: reviewText, rating: rating } 
      : event
    ));
    setModalVisible(false);
    setReviewText("");
    setRating(0);
  };

  const filteredEvents = events.filter(event => {
    if (filter === "all") return true;
    if (filter === "signedUp" && event.signedUp) return true;
    if (filter === "reviewed" && event.review) return true;
    return false;
  });

  return (
    <div className="App">
      {/* Header */}
      <header className="header">
        <nav>
          <ul>
            <li><a href="/">Homepage</a></li>
            <li><a href="/calendar">Calendar</a></li>
            <li><a href="/logout">Logout</a></li>
          </ul>
        </nav>
      </header>

      {/* Banner Image */}
      <div className="banner">
        <img src="\public\images\Events.jpg" alt="Banner" className="banner-image" />
      </div>

      {/* Filter for Events */}
      <div className="filters">
        <button onClick={() => setFilter("all")}>All Events</button>
        <button onClick={() => setFilter("signedUp")}>Signed-Up Events</button>
        <button onClick={() => setFilter("reviewed")}>Reviewed Events</button>
      </div>

      {/* Event Table */}
      <table className="events-table">
        <thead>
          <tr>
            <th>Event Name</th>
            <th>Place</th>
            <th>Date</th>
            <th>Time</th>
            <th>Description</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {filteredEvents.map((event) => (
            <tr key={event.id}>
              <td>{event.name}</td>
              <td>{event.place}</td>
              <td>{event.date}</td>
              <td>{event.time}</td>
              <td>{event.description}</td>
              <td>
                {!event.signedUp ? (
                  <button onClick={() => handleSignUp(event.id)}>Sign Up</button>
                ) : (
                  <>
                    <button onClick={() => handleRemoveSignUp(event.id)}>Remove Signup</button>
                    <button onClick={() => handleReview(event.id)}>
                      {event.review ? "Reviewed" : "Review"}
                    </button>
                  </>
                )}
                {event.review && <span>⭐ {event.rating} - {event.review}</span>}
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* Footer */}
      <footer className="footer">
        <ul>
          <li><a href="/contact">Contact</a></li>
          <li><a href="/info">Information</a></li>
          <li><a href="/report-bug">Report Bug</a></li>
        </ul>
      </footer>

      {/* Review Modal */}
      {modalVisible && (
        <div className="modal">
          <div className="modal-content">
            <h2>Leave a Review</h2>
            <textarea 
              value={reviewText} 
              onChange={(e) => setReviewText(e.target.value)} 
              placeholder="Write your review..." 
            />
            <div>
              <label>Rating: </label>
              <select value={rating} onChange={(e) => setRating(e.target.value)}>
                {[1, 2, 3, 4, 5].map((star) => (
                  <option key={star} value={star}>{star} Stars</option>
                ))}
              </select>
            </div>
            <button onClick={submitReview}>Submit Review</button>
            <button onClick={() => setModalVisible(false)}>Close</button>
          </div>
        </div>
      )}
    </div>
  );
}

export default App;