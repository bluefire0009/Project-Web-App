import { CalendarEvent } from "../States/CalendarState";
import Reviews from "./Reviews";
import SelectedEvent from "./SelectedEvent";
import SignUpSection from "./SignUpSection";

interface EventOverlayProps {
    event: CalendarEvent | undefined;
    isVisible: boolean;
    onClose: () => void;
  }

export const EventOverlay: React.FC<EventOverlayProps> = ({ event, isVisible, onClose }) => {        
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
            <Reviews />
            <SignUpSection />
          </div>
        </div>
      );
    };