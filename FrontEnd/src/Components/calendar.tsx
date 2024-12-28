// src/Calendar.js
import React from "react";
import { format, startOfMonth, endOfMonth, eachDayOfInterval, addMonths, subMonths, addDays, isSameDay, isSameMonth,  } from "date-fns";
import { CalendarEvent, CalendarState } from "../States/CalendarState";
import { InitCalendarState } from "../States/Updaters/CalendarUpdaters";
import SelectedEvent from "./SelectedEvent";
import Reviews from "./Reviews";
import SignUpSection from "./SignUpSection";

export class Calendar extends React.Component<{},CalendarState>{
    constructor(props:{}){
        super(props)
        this.state = InitCalendarState()
    }
    
    render(): React.ReactNode{
        return<div className="wholeCalendar">
            {this.renderCalendarHeader()}
            {this.renderDays()}
            {this.renderCells()}
            {this.renderEventOverlay(this.state.selectedEvent)}
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

    renderEventOverlay(event: CalendarEvent | undefined): JSX.Element | undefined{
        if (this.state.isEventOverlayVisible == false || event == undefined) {
            return undefined; // Don't render the overlay if it's not visible or there's no selected event
        }
        
        return <div className="overlay">
                <div className="overlay-content">
                    <button className="close-button" onClick={() => this.setState(this.state.toggleEventOverlay(false))}>✖</button>
                    {SelectedEvent(event)}
                    <Reviews />
                    <SignUpSection />
                </div>
            </div>
    }
}

export class CalendarSiteHeader extends React.Component{
    constructor(props:{}){
        super(props)
    }
    
    render(): React.ReactNode{
        return<header className="CalendarSiteHeader"
            style={{ textAlign: 'left', marginTop: '4px', paddingBottom: '2px', fontSize: '20px',  position: 'relative'}}>
                <a href="#" style={{ margin: '0 14px', textDecoration: 'none', color: '#000000' }}>
                Homepage
                </a>
                <a href="#" style={{ margin: '0 14px', textDecoration: 'none', color: '#000000' }}>
                Events
                </a>
                <a href="#" style={{ margin: '0 14px', textDecoration: 'none', color: '#000000' }}>
                Log out
                </a>
                {/* Rounded line container */}
                <div
                    style={{
                        position: 'absolute',
                        top: '100%',
                        left: '0',
                        width: '100%',
                        height: '2px',
                        backgroundColor: '#89898967',
                        borderRadius: '4px', // Rounds the ends of the line
                    }}
                />
        </header>
    }
}

export class CalendarSiteFooter extends React.Component{
    constructor(props:{}){
        super(props)
    }
    
    render(): React.ReactNode{
        return <footer style={{ textAlign: 'center', marginTop: '50px', fontSize: '14px' }}>
          <a href="#" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }}>
            Contact
          </a>
          <a href="#" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }}>
            Information
          </a>
          <a href="#" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }}>
            Report bug
          </a>
        </footer>
    }
}