import { useState } from "react"
import { Footer } from "./Footer"
import { Header } from "./Header"
import { WeekCalendar } from "./WeekCalendar"
import { MonthCalendar } from "./MonthCalendar"
import { HourDisplay } from "./HourDisplay"

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
        {/* <Header/> */}
        
        <ChangeCalendarButtons/>
        <div className="CalendarSection">
            {selectedCalendar == CurrentlySelectedCalendar.WeeklyCalendar? <span style={{display:"flex"}}><HourDisplay/><WeekCalendar/></span>:<MonthCalendar/>}
        </div>
        
        {/* <Footer/> */}
    </div>
}

const enum CurrentlySelectedCalendar{
    WeeklyCalendar,
    MonthlyCalendar
}