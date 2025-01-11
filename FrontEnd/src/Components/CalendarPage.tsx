import { useEffect, useState } from "react"
import { Footer } from "./Footer"
import { WeekCalendar } from "./WeekCalendar"
import { MonthCalendar } from "./MonthCalendar"
import { HourDisplay } from "./HourDisplay"
import { CalendarEvent } from "../States/CalendarState"
import { fetchAllEvents } from "../DataLoader"

export const CalendarPage: React.FC = () =>{
    const [selectedCalendar, setSelectedCalendar] = useState<CurrentlySelectedCalendar>(CurrentlySelectedCalendar.WeeklyCalendar)
    const [events, setCurrentEvents] = useState<CalendarEvent[]>([]);

    useEffect(() => {
        const loadEvents = async () => {
            try {
                const fetchedEvents = await fetchAllEvents();
                console.log("------")
                setCurrentEvents(fetchedEvents); // Update the state with fetched events
                
            } catch (error) {
                setCurrentEvents([]);
                console.log(error)
            }
        };
        loadEvents();
        console.log(events);
    }, []);

    const ChangeCalendarButtons: React.FC = () =>{
        return <span className="CalendarButtons">
            <button className="WeeklyCalendarButton"
            onClick={_ => {setSelectedCalendar(CurrentlySelectedCalendar.WeeklyCalendar)}}
            disabled={selectedCalendar == CurrentlySelectedCalendar.WeeklyCalendar}>
                Weekly Calendar</button>

            <button className="MonthlyCalendarButton"
            onClick={_ => {setSelectedCalendar(CurrentlySelectedCalendar.MonthlyCalendar)}}
            disabled={selectedCalendar == CurrentlySelectedCalendar.MonthlyCalendar}>
                Monthly Calendar</button>
        </span>
    }
    return <div className="CalendarPage">
        <ChangeCalendarButtons/>
        <div className="CalendarSection">
            {selectedCalendar == CurrentlySelectedCalendar.WeeklyCalendar? <span style={{display:"flex"}}><HourDisplay/><WeekCalendar events={events}/></span>:<MonthCalendar events={events}/>}
        </div>
    </div>
}

const enum CurrentlySelectedCalendar{
    WeeklyCalendar,
    MonthlyCalendar
}