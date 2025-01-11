import { CalendarEvent } from "./States/CalendarState";
import Api_url from "./Components/Api_url"

export const fetchAllEvents:()=> Promise<CalendarEvent[]> = async () => await fetch(`${Api_url}/api/events/get/all`)
        .then(response => response.json())
        