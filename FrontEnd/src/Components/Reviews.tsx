import React, { useEffect, useState } from 'react';
import ReactPaginate from 'react-paginate';
import Api_url from './Api_url'; // Make sure this is correctly configured

const Reviews: React.FC = () => {
  const [reviews, setReviews] = useState<any[]>([]);
  const [currentPage, setCurrentPage] = useState(0);
  const [loading, setLoading] = useState(false);
  const itemsPerPage = 4; // Number of reviews per page

  // Fetch reviews from the backend
  useEffect(() => {
    const fetchReviews = async () => {
      setLoading(true);
      try {
        const response = await fetch(`${Api_url}/api/eventAttendance/get?id=1`);
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        console.log(response);
        const data = await response.json();
        console.log(data);
        setReviews(data);
      } catch (error) {
        console.error('Failed to fetch reviews:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchReviews();
  }, []);

  // Calculate current reviews to display
  const offset = currentPage * itemsPerPage;
  const currentReviews = reviews.slice(offset, offset + itemsPerPage);

  // Handle page change
  const handlePageClick = (event: { selected: number }) => {
    setCurrentPage(event.selected);
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
              {currentReviews.map((review, index) => (
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
            previousLabel={<button style={{ border: '0px', outline: 'none', color: '#007bff' }} className="prev">← Previous</button>}
            nextLabel={<button style={{ border: '0px', outline: 'none', color: '#007bff' }} className="next">Next →</button>}
            pageCount={Math.ceil(reviews.length / itemsPerPage)}
            onPageChange={handlePageClick}
            containerClassName={"pagination"}
            activeClassName={"active"}
            pageClassName={"page"}
            previousClassName={"prev"}
            nextClassName={"next"}
            forcePage={currentPage}
            disabledClassName={"disabled"}
          />
        </>
      )}
    </div>
  );
};

export default Reviews;



