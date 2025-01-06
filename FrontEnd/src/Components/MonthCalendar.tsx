// src/Calendar.js
import React from "react";
import { format, startOfMonth, endOfMonth, eachDayOfInterval, addMonths, subMonths, addDays, isSameDay, isSameMonth,  } from "date-fns";
import { CalendarState } from "../States/CalendarState";
import { InitCalendarState } from "../States/Updaters/CalendarUpdaters";
import { EventOverlay } from "./EventSignupOverlay";

export class MonthCalendar extends React.Component<{},CalendarState>{
    constructor(props:{}){
        super(props)
        this.state = InitCalendarState()
    }
    
    render(): React.ReactNode{
        return<div className="wholeCalendar">
            <EventOverlay
            event={this.state.selectedEvent}
            isVisible={this.state.isEventOverlayVisible}
            onClose={() => this.setState(this.state.toggleEventOverlay(false))}
            />
            {this.renderCalendarHeader()}
            {this.renderDays()}
            {this.renderCells()}
        </div>
    }

    renderCalendarHeader(): JSX.Element{
        return (
            <div className="calendar-header">
                <button 
                    onClick={() => {
                                    this.setState(this.state.setCurrentDate(subMonths(this.state.currentDate, 1)))
                                    this.setState(this.state.setCurrentMonth(subMonths(this.state.currentDate, 1)))    
                                }}>
                    ❮
                </button>
                <h2>{format(this.state.currentDate, "MMMM yyyy")} </h2>
                <button 
                    onClick={() => {
                                    this.setState(this.state.setCurrentDate(addMonths(this.state.currentDate, 1)))
                                    this.setState(this.state.setCurrentMonth(addMonths(this.state.currentDate, 1))) 
                                }}>
                    ❯
                </button>
            </div>
        );
    }

    renderDays(): JSX.Element{
        return (
            <div className="days-row">
              {["Sunday", "Monday", "Tuesday", "Wednsday", "Thursday", "Friday", "Saturday"].map((day) => (
                <div className="day-name" key={day}>
                  {day}
                </div>
              ))}
            </div>
          );
    }

    renderCells(): JSX.Element{
        return <div className="body">{
            this.state.currentMonth.map(
            (date, index) => ( isSameMonth(date, this.state.currentDate) || date != undefined? 
                <button className={`cell ${isSameDay(date, new Date()) ? "today" : ""}`} key={date.toISOString()} 
                 onClick={(_) => {
                    const event = this.state.currentEvents.find(e => isSameDay(e.StartTime, date));
                    if (event) {
                        this.setState(this.state.setSelectedEvent(event))
                        this.setState(this.state.toggleEventOverlay(true))
                    }
                }} 
                 onMouseOver={()=>{this.state.setSelectedEvent(this.state.currentEvents[0]/*.find(e => e.StartTime == date? true: false)*/)}}>
                    <span className="day-number">{format(date, "d")}</span>
                    {this.state.currentEvents.some(event => isSameDay(event.StartTime, date)) && <span className="event-text">event</span>}
                </button>
                :
                <div className="cell empty" key={`empty-${index}`} />)
        )}</div>
    }
}