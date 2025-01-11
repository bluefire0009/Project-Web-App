// src/Calendar.js
import React from "react";
import { format, startOfMonth, endOfMonth, eachDayOfInterval, addMonths, subMonths, addDays, isSameDay, isSameMonth,  } from "date-fns";
import { CalendarEvent, CalendarEventConstructor, MonthCalendarState } from "../States/CalendarState";
import { InitCalendarState } from "../States/Updaters/CalendarUpdaters";
import { EventOverlay } from "./EventSignupOverlay";
import { EventSelector } from "./EventSelector";

export class MonthCalendar extends React.Component<{events:CalendarEvent[]},MonthCalendarState>{
    constructor(props:{events:CalendarEvent[]}){
        super(props)
        this.state = {
            ...InitCalendarState(),
            currentEvents: props.events, // Initialize currentEvents from props
        }
    }
    
    render(): React.ReactNode{
        return<div className="wholeCalendar">
            <EventOverlay
            event={this.state.selectedEvent}
            isVisible={this.state.isEventOverlayVisible}
            onClose={() => this.setState(this.state.toggleEventOverlay(false))}
            />
            <EventSelector
            events={this.state.selectedSelectorEvents}
            isVisible={this.state.isEventSelectorVisible}
            onClose={() => this.setState(this.state.toggleEventSelector(false))}
            />

            {this.renderCalendarHeader()}
            {this.renderDays()}
            {this.renderCells()}
        </div>
    }

    renderCalendarHeader(): JSX.Element{
        return (
            <div className="calendar-header">
                <button style={{marginBottom: "5px", marginTop: "5px", backgroundColor: "rgb(100, 100, 100)"}}
                    onClick={() => {
                                    this.setState(this.state.setCurrentDate(subMonths(this.state.currentDate, 1)))
                                    this.setState(this.state.setCurrentMonth(subMonths(this.state.currentDate, 1)))    
                                }}>
                    ❮
                </button>
                <h2>{format(this.state.currentDate, "MMMM yyyy")} </h2>
                <button style={{marginBottom: "5px", marginTop: "5px", backgroundColor: "rgb(100, 100, 100)"}}
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
                    const eventsOnThisDay = this.state.currentEvents.filter(e => isSameDay(e.StartTime, date));
                    if (eventsOnThisDay.length < 1){

                    }
                    else if (eventsOnThisDay.length > 1) {
                        this.setState(this.state.setSelectedEvents(eventsOnThisDay));
                        this.setState(this.state.toggleEventSelector(true));
                    }
                    else{
                        const event = eventsOnThisDay[0];
                        this.setState(this.state.setSelectedEvent(event));
                        this.setState(this.state.toggleEventOverlay(true));
                    }
                }} 
                 onMouseOver={()=>{this.state.setSelectedEvent(this.state.currentEvents[0]/*.find(e => e.StartTime == date? true: false)*/)}}>
                    <span className="day-number">{format(date, "d")}</span>
                    {this.state.currentEvents.some(event => isSameDay(event.StartTime, date)) && (
                    <span className="event-text">
                        {(() => {
                            const eventCount = this.state.currentEvents.reduce(
                            (accumulator, currentEvent) =>
                                isSameDay(currentEvent.StartTime, date) ? accumulator + 1 : accumulator,0);
                            return eventCount === 1 ? `${eventCount} event` : `${eventCount} events`;
                        })()}
                    </span>
                    )}
                </button>
                :
                <div className="cell empty" key={`empty-${index}`} />)
        )}</div>
    }
}