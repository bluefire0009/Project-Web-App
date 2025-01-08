import React from 'react';
import { CalendarEvent } from '../States/CalendarState';
import { format } from 'date-fns';

export const SelectedEvent: React.FC<CalendarEvent> = (event: CalendarEvent) => {
  return (
    <div>
      <div>Here you can see more information about the selected event.</div>
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
            <td>{event.Title}</td>
            <td>{event.Location}</td>
            <td>{format(event.StartTime, 'dd/MM/yyyy')}</td>
            <td>{format(event.StartTime, "kk:mm")} - {format(event.EndTime, "kk:mm")}</td>
            <td>{event.Description}</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default SelectedEvent;