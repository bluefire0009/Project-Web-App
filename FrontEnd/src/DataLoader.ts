import { CalendarEvent, CalendarEventConstructor } from "./States/CalendarState";
import Api_url from "./Components/Api_url"

export const fetchAllEvents:()=> Promise<CalendarEvent[]> = async () => {
        const events = await fetch(`${Api_url}/api/events/get/all`)
        .then(response => response.json())
        const parsedEvents: CalendarEvent[] = events.map((event: any) => 
                CalendarEventConstructor(
                        BigInt(event.eventId),
                        event.title,
                        event.description,
                        new Date(`${event.eventDate}T${event.startTime}`),
                        new Date(`${event.eventDate}T${event.endTime}`),
                        event.location
                ));
        return parsedEvents;
}
        