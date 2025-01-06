import React, { useEffect, useState } from "react";
import { format, addDays, subDays, startOfWeek, getWeek, interval, eachDayOfInterval, isSameDay } from "date-fns";
import { CalendarEvent } from "../States/CalendarState";
import { EventOverlay } from "./EventSignupOverlay";

export const WeekCalendar: React.FC = () => {
    const [selectedDate, setSelectedWeek] = useState<Date>(startOfWeek(new Date()));
    const [eventOverlaysVisible, toggleEventOverlay] = useState<boolean>(false);
    const [selectedEvent, setSelectedEvent] = useState<CalendarEvent | undefined>(undefined);
    const [currentEvents, setCurrentEvents] = useState<CalendarEvent[]>([
        {EventId:1n,Description:"Company party for christmas", Location:"First floor", Title: "Christmas party", StartTime: new Date("2024/12/24 14:00"), EndTime: new Date("2024/12/24 15:00")},
        {EventId:2n,Description:"Teamwork workshop given by company A", Location:"First floor", Title: "Workshop", StartTime: new Date("2024/12/8 14:00"), EndTime: new Date("2024/12/8 15:00")},
        {EventId:3n,Description:"Conflict resolution workshop given by company B", Location:"First floor", Title: "Workshop", StartTime: new Date("2024/12/9 14:00"), EndTime: new Date("2024/12/9 15:00")},
        {EventId:4n,Description:"Company party for halloween", Location:"Second floor", Title: "Halloween party", StartTime: new Date("2024/12/14 14:00"), EndTime: new Date("2024/12/14 16:00")},
        {EventId:5n,Description:"Company party for halloween", Location:"Second floor", Title: "Halloween party", StartTime: new Date("2024/12/13 7:45"), EndTime: new Date("2024/12/13 12:00")},
        {EventId:6n,Description:"Company party for halloween", Location:"Second floor", Title: "Halloween party", StartTime: new Date("2024/12/13 11:45"), EndTime: new Date("2024/12/13 16:00")},
        {EventId:7n,Description:"Company party for halloween", Location:"Second floor", Title: "Halloween party", StartTime: new Date("2024/12/12 10:00"), EndTime: new Date("2024/12/12 16:00")},
    ]);

    const WeekBar: React.FC = () => {
        const paddingLeftRight = "100px"
        return <div className="WeekBar">
            <button style={{marginBottom: "5px", marginTop: "5px"}} 
            onClick={_ => setSelectedWeek(subDays(selectedDate, 7))}>
                ← Previous
            </button>


            <h2 style={{paddingLeft: paddingLeftRight, paddingRight: paddingLeftRight}}>
                Week {getWeek(selectedDate)}
            </h2>

            <button style={{marginBottom: "5px", marginTop: "5px"}}
            onClick={_ => setSelectedWeek(addDays(selectedDate, 7))}>
                Next →
            </button>
        </div>
    }

    const WeekHeader: React.FC = () =>{
        const currentWeek: Date[] = eachDayOfInterval(interval(addDays(selectedDate,2), addDays(selectedDate,8)))
        return <div className="WeekHeader">
            {currentWeek.map((day) => (
                <div className="dateDisplay" key={day.toString()}>
                  {day.toUTCString().split(" ").slice(0,3).join(" ")}
                </div>
            ))}
        </div>
    }

    const WeekGrid: React.FC = () => {
        const currentWeek: Date[] = eachDayOfInterval(interval(addDays(selectedDate,1), addDays(selectedDate,7)))
        const eventsThisWeek = currentEvents.filter((event)=> event.StartTime>=currentWeek[0] && event.StartTime<=currentWeek[currentWeek.length-1])

        const getEventStyle = (event: CalendarEvent) => {
            const eventDayIndex = currentWeek.findIndex(day => isSameDay(day, event.StartTime));
            const hourHeight = 4.17;
            const top = (event.StartTime.getHours() * hourHeight) + ((event.StartTime.getMinutes() / 60) * hourHeight);
            const left = eventDayIndex * 14.25
            const height = (event.EndTime.getTime() - event.StartTime.getTime()) / (1000 * 60 * 60) * hourHeight;
            const width = 14.32
            return {
                top: `${top}%`,
                left: `${left}%`,
                height: `${height}%`,
                minWidth: `${width}%`,
                maxWidth: `${width}%`,
            };
        };
        
        return<div className="WeekGrid">
            
            {eventsThisWeek.map((event) => (
                <button className="WeekCalendarEvent"
                key={event.EventId.toString()}
                onMouseOver={_ => setSelectedEvent(event)}
                onClick={_ => toggleEventOverlay(true)}
                style={getEventStyle(event)}>
                    {<div style={{position:"absolute", top:"4px", left: "4px"}}>{event.Title}</div>}

                    {<div style={{position:"absolute", bottom:"20px", left: "4px", fontSize: "14px"}}>{format(event.StartTime, "kk:mm")} - {format(event.EndTime, "kk:mm")}</div>}
                    {<div style={{position:"absolute", bottom:"4px", left: "4px", fontSize: "14px"}}>{event.StartTime.toDateString()}</div>}
                </button>
            ))}

            {currentWeek.map((day) => (
                <div>
                  {Array.from({ length: 48 }, (_, i) => i + 1).map((hour) => 
                    hour%2 == 0 ? <div className="HourLine"/> : <div className="HalfHourLine"/>
                  )}  
                </div>
            ))}
        </div>
    }
    
    return <div className="WeekCalendar">
        <EventOverlay
            event={selectedEvent}
            isVisible={eventOverlaysVisible}
            onClose={() => toggleEventOverlay(false)}
            />
        <WeekBar/>
        <WeekHeader/>
        <WeekGrid/>
        
    </div>
}
