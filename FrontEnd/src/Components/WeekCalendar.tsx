import React, { useEffect, useState } from "react";
import { format, addDays, subDays, startOfWeek, getWeek, interval, eachDayOfInterval, isSameDay } from "date-fns";
import { CalendarEvent } from "../States/CalendarState";
import { EventOverlay } from "./EventSignupOverlay";

export const WeekCalendar: React.FC<{events: CalendarEvent[]}> = ({events}) => {
    const [selectedDate, setSelectedWeek] = useState<Date>(startOfWeek(new Date()));
    const [eventOverlaysVisible, toggleEventOverlay] = useState<boolean>(false);
    const [selectedEvent, setSelectedEvent] = useState<CalendarEvent | undefined>(undefined);
    const [currentEvents, setCurrentEvents] = useState<CalendarEvent[]>(events);

    // this ensures that events are updated when they are fetched in CalendarPage component
    useEffect(() => {
        setCurrentEvents(events);
    }, [events]);

    const WeekBar: React.FC = () => {
        const paddingLeftRight = "100px"
        return <div className="WeekBar">
            <button style={{position: "fixed", right: "60%", width: "10%", marginBottom: "5px", marginTop: "5px", backgroundColor: "rgb(100, 100, 100)", zIndex: "10000"}} 
            onClick={_ => setSelectedWeek(subDays(selectedDate, 7))}>
                ← Previous
            </button>


            <h2 style={{paddingLeft: paddingLeftRight, paddingRight: paddingLeftRight}}>
                Week {getWeek(selectedDate)}
            </h2>

            <button style={{position: "fixed", left: "60%", width: "10%", marginBottom: "5px", marginTop: "5px", backgroundColor: "rgb(100, 100, 100)", zIndex: "10000"}}
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
        const eventsThisWeek = currentEvents.filter((event)=> event.StartTime>=currentWeek[0] && event.StartTime<addDays(currentWeek[currentWeek.length-1],1))

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
