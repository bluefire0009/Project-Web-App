import React from 'react';

const SelectedEvent: React.FC = () => {
  const event = {
    title: 'WebDev React les 2',
    place: 'Hogeschool Rotterdam',
    date: '20-07-2025',
    time: '9:00 - 18:00',
    description: 'Attendence necesary. Make sure to have created a React component. 1 per person',
  };

  return (
    <div>
      <h2>Selected Event</h2>
      <p>Here you can see more information about the selected event.</p>
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
            <td>{event.title}</td>
            <td>{event.place}</td>
            <td>{event.date}</td>
            <td>{event.time}</td>
            <td>{event.description}</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default SelectedEvent;