import { CalendarEvent, CalendarState } from "../CalendarState";
import { format, startOfMonth, endOfMonth, eachDayOfInterval, addMonths, subMonths, addDays, isSameDay, isSameMonth,  } from "date-fns";

export const InitCalendarState = () : CalendarState => ({
    currentDate: new Date(),
    currentMonth: eachDayOfInterval({start:startOfMonth(new Date()), end:endOfMonth(new Date())}),
    currentEvents: [
                    {EventId:1n,Description:"Company party for christmas", Location:"First floor", Title: "Christmas party", StartTime: new Date("2024/12/24 14:00"), EndTime: new Date("2024/12/24 15:00")},
                    {EventId:2n,Description:"Teamwork workshop given by company A", Location:"First floor", Title: "Workshop", StartTime: new Date("2024/12/8 14:00"), EndTime: new Date("2024/12/24 15:00")},
                    {EventId:3n,Description:"Conflict resolution workshop given by company B", Location:"First floor", Title: "Workshop", StartTime: new Date("2024/12/9 14:00"), EndTime: new Date("2024/12/24 15:00")},
                    {EventId:4n,Description:"Company party for halloween", Location:"Second floor", Title: "Halloween party", StartTime: new Date("2024/12/14 14:00"), EndTime: new Date("2024/12/14 16:00")}
                ],
    selectedEvent: undefined,
    isEventOverlayVisible: true,
    setCurrentDate: (date: Date) => (currentState: CalendarState) => ({...currentState, currentDate: date}),
    setCurrentMonth: (date: Date) => (currentState: CalendarState) => {
        const startOfMonthDate = startOfMonth(date);
        const endOfMonthDate = endOfMonth(date);
        const daysInMonth = eachDayOfInterval({ start: startOfMonthDate, end: endOfMonthDate });
        const startDay = startOfMonthDate.getDay();

        const emptyCells = Array(startDay).fill(undefined);
        const currentMonth = [...emptyCells, ...daysInMonth];

        return {
            ...currentState,
            currentDate: date,
            currentMonth,
        };
    },
    setSelectedEvent: (event: CalendarEvent | undefined) => (currentState: CalendarState) => (event!=undefined?{...currentState, selectedEvent: event}:{...currentState}),
    toggleEventOverlay: (visible: boolean) => (currentState: CalendarState) => ({ ...currentState, isEventOverlayVisible: visible })
})