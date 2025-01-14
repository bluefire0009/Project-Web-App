import React, { useEffect, useState } from 'react';
import ReactPaginate from 'react-paginate';
import Api_url from './Api_url'; // Make sure this is correctly configured
import { Review, ReviewConstructor } from '../States/ReviewState';

const Reviews: React.FC = () => {
  const [reviews, setReviews] = useState<any[]>([]);
  const [currentPage, setCurrentPage] = useState(0);
  const [loading, setLoading] = useState(false);
  const [showModal, setShowModal] = useState(false); // State to show/hide the modal
  const [newReview, setNewReview] = useState({
    rating: '',
    review: '',
  }); // State for the new review form
  const itemsPerPage = 4; // Number of reviews per page

  // Fetch reviews from the backend
  useEffect(() => {
    const fetchReviews = async () => {
      setLoading(true);
      try {
        const response = await fetch(`${Api_url}/api/EventAttendance/GetByEvent/1`);
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const data = await response.json();
        const parsedData: Review[] = data.map((review: any) =>
          ReviewConstructor(
            review.user.firstName,
            review.datePlaced,
            review.event.eventDate,
            review.feedback,
            review.rating
          )
        );
        setReviews(parsedData);
      } catch (error) {
        console.error('Failed to fetch reviews:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchReviews();
  }, []);

  // Handle page change
  const handlePageClick = (event: { selected: number }) => {
    setCurrentPage(event.selected);
  };

  // Handle modal open/close
  const handleModal = (state: boolean) => {
    setShowModal(state);
  };

  // Handle form submission
  const handleSubmitReview = async (event: React.FormEvent) => {
    event.preventDefault();
    
    if (!newReview.rating || !newReview.review) {
      alert('Please fill in all fields before submitting.');
      return;
    }
  
    try {
      const response = await fetch(
        `${Api_url}/api/Attendance/Review?eventId=1&rating=${newReview.rating}&review=${encodeURIComponent(newReview.review)}`,
        {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          credentials: "include"
        }
      );
  
      if (response.ok) {
        alert('Review submitted successfully!');
        setNewReview({ rating: '', review: '' });
        setShowModal(false);
      } else {
        const errorMessage = await response.text();
        alert(`Failed to submit review: ${errorMessage}`);
      }
    } catch (error) {
      console.error('Error submitting review:', error);
      alert('An error occurred while submitting the review.');
    }
  };

  return (
    <div>
      <h2>Reviews</h2>
      {loading ? (
        <p>Loading reviews...</p>
      ) : (
        <>
          <table>
            <thead>
              <tr>
                <th>User</th>
                <th>Date event</th>
                <th>Date placed</th>
                <th>Stars</th>
                <th>Review</th>
              </tr>
            </thead>
            <tbody>
              {reviews.map((review, index) => (
                <tr key={index}>
                  <td>{review.user}</td>
                  <td>{review.dateEvent}</td>
                  <td>{review.datePlaced}</td>
                  <td>{review.stars}</td>
                  <td>{review.review}</td>
                </tr>
              ))}
            </tbody>
          </table>
          <ReactPaginate
            previousLabel={<button style={{ border: '0px', outline: 'none', color: '#007bff' }}>← Previous</button>}
            nextLabel={<button style={{ border: '0px', outline: 'none', color: '#007bff' }}>Next →</button>}
            pageCount={Math.ceil(reviews.length / itemsPerPage)}
            onPageChange={handlePageClick}
            containerClassName={'pagination'}
            activeClassName={'active'}
            pageClassName={'page'}
            previousClassName={'prev'}
            nextClassName={'next'}
            forcePage={currentPage}
            disabledClassName={'disabled'}
          />
        </>
      )}
      <button onClick={() => handleModal(true)} style={{ marginTop: '20px' }}>
        Post a Review
      </button>
      {showModal && (
        <div className="modal">
          <div className="modal-content">
            <h3>Post a Review</h3>
            <form onSubmit={handleSubmitReview}>
              <label>
                Rating:
                <input
                  type="number"
                  min="1"
                  max="5"
                  value={newReview.rating}
                  onChange={(e) => setNewReview({ ...newReview, rating: e.target.value })}
                  required
                />
              </label>
              <label>
                Review:
                <textarea
                  value={newReview.review}
                  onChange={(e) => setNewReview({ ...newReview, review: e.target.value })}
                  required
                />
              </label>
              <button type="submit">Submit</button>
              <button type="button" onClick={() => handleModal(false)}>
                Cancel
              </button>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default Reviews;