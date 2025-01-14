import { useEffect, useState } from "react";
import { CalendarEvent } from "../States/CalendarState";
import Reviews from "./Reviews";
import SelectedEvent from "./SelectedEvent";
import SignUpSection from "./SignUpSection";
import { fetchUserId } from "./getUserName";

interface EventOverlayProps {
    event: CalendarEvent | undefined;
    isVisible: boolean;
    onClose: () => void;
  }

export const EventOverlay: React.FC<EventOverlayProps> = ({ event, isVisible, onClose }) => {        
    const [userId, setUserId] = useState<number>(0);
    
    useEffect(() => {
            const loadId = async () => {
              const userId = await fetchUserId();  
              setUserId(typeof userId === 'number' ? userId : -1);
            };
            loadId();
        }, []);

    if (!isVisible || !event) {
        return null; // Don't render the overlay if it's not visible or there's no selected event
      }
    
      return (
        <div className="overlay">
          <div className="overlay-content">
            <button className="close-button" onClick={onClose}>
              ✖
            </button>
            {SelectedEvent(event)}
            <Reviews eventId={event.EventId} />
            <SignUpSection event={event} currentUserId={userId} />
          </div>
        </div>
      );
    };