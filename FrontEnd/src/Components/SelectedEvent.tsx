import React from 'react';
import { CalendarEvent } from '../States/CalendarState'; // Importing the CalendarEvent interface/type
import { format } from 'date-fns'; // Importing the date-fns library for date formatting

// SelectedEvent component to display detailed information about a specific calendar event
export const SelectedEvent: React.FC<CalendarEvent> = (event: CalendarEvent) => {
  return (
    <div>
      {/* Header explaining the purpose of the component */}
      <div>Here you can see more information about the selected event.</div>
      
      {/* Table to display event details */}
      <table>
        <thead>
          {/* Table headers for event details */}
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
            {/* Displaying the event details */}
            <td>{event.Title}</td> {/* Event title */}
            <td>{event.Location}</td> {/* Event location */}
            <td>{format(event.StartTime, 'dd/MM/yyyy')}</td> {/* Event date formatted as dd/MM/yyyy */}
            <td>
              {format(event.StartTime, "kk:mm")} - {format(event.EndTime, "kk:mm")}
            </td> {/* Event time range formatted as hours:minutes */}
            <td>{event.Description}</td> {/* Event description */}
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default SelectedEvent;
