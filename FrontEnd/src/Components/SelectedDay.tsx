import React from 'react';

const SelectedDay: React.FC = () => {
  // Simulated work data
  const workDetails = [
    {
      work: 'WebDev FrontEnd pagina maken van pagina 8',
      place: 'Thuis',
      date: 'Zekers niet op de zondag avond',
      time: '16.00 - 23:00',
      description: 'Gewoon pagina maken die je al eens eerder hebt gemaakt, Je kan het :D',
    },
  ];

  return (
    <div className="container">
      <header className="navbar">
        <a href="#">Homepage</a>
        <a href="#">Events</a>
        <a href="#">Calendar</a>
        <a href="#">Log out</a>
      </header>

      <main>
        <section>
          <h1>Selected day</h1>
          <p>Here you can see more information about the selected work day.</p>
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

        <section>
          <h2>Remove attendance</h2>
          <p>Remove your attendance</p>
          <button className="remove-btn">REMOVE</button>
        </section>
      </main>

      <footer>
        <a href="#">Contact</a>
        <a href="#">Information</a>
        <a href="#">Report bug</a>
      </footer>
    </div>
  );
};

export default SelectedDay;