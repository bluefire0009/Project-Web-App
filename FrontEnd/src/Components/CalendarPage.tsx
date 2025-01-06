import { useState } from "react"
import { Footer } from "./Footer"
import { Header } from "./Header"
import { WeekCalendar } from "./WeekCalendar"
import { MonthCalendar } from "./MonthCalendar"

export const CalendarPage: React.FC = () =>{
    const [selectedCalendar, setSelectedCalendar] = useState<CurrentlySelectedCalendar>(CurrentlySelectedCalendar.WeeklyCalendar)
    
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
        <Header/>
        <div className="CalendarSection">
            <ChangeCalendarButtons/>
            {selectedCalendar == CurrentlySelectedCalendar.WeeklyCalendar? <WeekCalendar/>:<MonthCalendar/>}
        </div>
        
        <Footer/>
    </div>
}

const enum CurrentlySelectedCalendar{
    WeeklyCalendar,
    MonthlyCalendar
}