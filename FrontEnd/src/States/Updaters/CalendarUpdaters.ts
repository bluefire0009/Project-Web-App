import { CalendarEvent, MonthCalendarState } from "../CalendarState";
import { format, startOfMonth, endOfMonth, eachDayOfInterval, addMonths, subMonths, addDays, isSameDay, isSameMonth,  } from "date-fns";

export const InitCalendarState = () : MonthCalendarState => ({
    currentDate: new Date(),
    currentMonth: eachDayOfInterval({start:startOfMonth(new Date()), end:endOfMonth(new Date())}),
    currentEvents: [],
    selectedEvent: undefined,
    selectedSelectorEvents: [],
    isEventOverlayVisible: false,
    isEventSelectorVisible: false,
    setCurrentDate: (date: Date) => (currentState: MonthCalendarState) => ({...currentState, currentDate: date}),
    setCurrentMonth: (date: Date) => (currentState: MonthCalendarState) => {
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
    setCurrentEvents: (events: CalendarEvent[]) => (currentState: MonthCalendarState) => ({...currentState, currentEvents: events}),
    setSelectedEvent: (event: CalendarEvent | undefined) => (currentState: MonthCalendarState) => (event!=undefined?{...currentState, selectedEvent: event}:{...currentState}),
    setSelectedEvents: (events: CalendarEvent[]) => (currentState: MonthCalendarState) => ({ ...currentState, selectedSelectorEvents: events }),
    toggleEventOverlay: (visible: boolean) => (currentState: MonthCalendarState) => ({ ...currentState, isEventOverlayVisible: visible }),
    toggleEventSelector: (visible: boolean) => (currentState: MonthCalendarState) => ({ ...currentState, isEventSelectorVisible: visible })
})