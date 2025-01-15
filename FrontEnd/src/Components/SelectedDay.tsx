import React from 'react';

// Component to display details of a selected work day
const SelectedDay: React.FC = () => {
  // Simulated work data for the selected day
  const workDetails = [
    {
      work: 'WebDev FrontEnd pagina maken van pagina 8', // Work task
      place: 'Thuis', // Work location
      date: 'Zekers niet op de zondag avond', // Work date
      time: '16.00 - 23:00', // Work hours
      description: 'Gewoon pagina maken die je al eens eerder hebt gemaakt, Je kan het :D', // Description of the task
    },
  ];

  return (
    <div className="container">
      <main>
        {/* Section to display information about the selected day */}
        <section>
          <h1>Selected Day</h1>
          <p>Here you can see more information about the selected work day.</p>
          
          {/* Table to display work details */}
          <table className="info-table">
            <thead>
              <tr>
                <th>Work</th>
                <th>Place</th>
                <th>Date</th>
                <th>Time</th>
                <th>Description</th>
              </tr>
            </thead>
            <tbody>
              {/* Dynamically rendering work details */}
              {workDetails.map((work, index) => (
                <tr key={index}>
                  <td>{work.work}</td>
                  <td>{work.place}</td>
                  <td>{work.date}</td>
                  <td>{work.time}</td>
                  <td>{work.description}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </section>

        {/* Section for removing attendance */}
        <section>
          <h2>Remove Attendance</h2>
          <p>Remove your attendance</p>
          {/* Button to trigger attendance removal (no functionality attached yet) */}
          <button className="remove-btn">REMOVE</button>
        </section>
      </main>
    </div>
  );
};

export default SelectedDay;
