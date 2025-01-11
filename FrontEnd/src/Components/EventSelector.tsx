import { useState } from "react";
import { CalendarEvent } from "../States/CalendarState"
import { EventOverlay } from "./EventSignupOverlay";
import { format } from "date-fns";

interface EventSelectorProps {
    events: CalendarEvent[];
    isVisible: boolean;
    onClose: () => void;
  }

export const EventSelector: React.FC<EventSelectorProps> = ({ events, isVisible, onClose }) => {        
    const [isEventOverlayVisible, toggleEventOverlay] = useState<boolean>(false);
    const [selectedEvent, selectEvent] = useState<CalendarEvent>();
    
    if (!isVisible) {
        return null; // Don't render the overlay if it's not visible or there's no selected event
      }
    
    return <div className="overlay">
        <EventOverlay
                event={selectedEvent}
                isVisible={isEventOverlayVisible}
                onClose={() => toggleEventOverlay(false)}
                />
        <div className="overlay-content">
            <button className="close-button" onClick={onClose}>
                ✖
            </button>
            <div className="eventList">
                <div style={{display:"flex", alignItems:"center"}}>
                    <div style={{position: "relative", margin: "10px", fontSize: "18px", fontWeight:"bolder", left:"30px"}}>
                        Event title</div>

                    <div style={{position:"relative", margin: "10px", fontSize: "18px", fontWeight:"bolder", left:"85px"}}>
                        Duration</div>
                </div>
                {events.map((currentEvent, index) => 
                <div className="eventInList" key={index} style={{display:"flex", alignItems:"center"}}>
                    <div style={{margin: "10px", fontSize: "18px", width: "15ch", wordWrap:"break-word"}}>
                        {currentEvent.Title}</div>

                    <div style={{margin: "10px", fontSize: "18px"}}>
                        {`${format(currentEvent.StartTime, "kk:mm")} - ${format(currentEvent.EndTime, "kk:mm")}`}</div>

                    <button style={{margin: "10px", padding: '10px 20px', backgroundColor: '#007BFF', color: 'white', border: 'none', borderRadius: '5px', cursor: 'pointer' }}
                    onClick={(_) => {selectEvent(currentEvent); toggleEventOverlay(true)}}>
                        Show more</button>
                </div>)}
            </div>
        </div>
    </div>
}