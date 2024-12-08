import React from "react";

function SelectedDay() {
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
              <tr>
                <td>WebDev FrontEnd pagina maken van pagina 8</td>
                <td>Thuis</td>
                <td>Zekers niet op de zondag avond</td>
                <td>16.00 - 23:00</td>
                <td>Gewoon  pagina make die je al is eerder hebt gemaakt, Je kan het :D</td>
              </tr>
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
}

export default SelectedDay;