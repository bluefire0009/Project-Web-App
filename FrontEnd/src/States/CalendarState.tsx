export type CalendarEvent = {
    EventId: bigint;
    Title: string;
    Description: string;
    StartTime: Date;
    EndTime: Date;
    Location: string;
};

export const CalendarEventConstructor = (id:bigint, title:string, desc:string, startTime:Date, endTime:Date, location:string) : CalendarEvent => ({
    EventId: id,
    Title: title,
    Description: desc,
    StartTime: startTime,
    EndTime: endTime,
    Location: location
})

export type MonthCalendarState = {
    currentDate: Date;
    currentMonth: Date[];
    currentEvents: CalendarEvent[];
    selectedEvent: CalendarEvent | undefined;
    selectedSelectorEvents: CalendarEvent[];
    isEventOverlayVisible: boolean;
    isEventSelectorVisible: boolean;
    setCurrentDate: (date: Date) => (currentState: MonthCalendarState) => MonthCalendarState;
    setCurrentMonth: (date: Date) => (currentState: MonthCalendarState) => MonthCalendarState;
    setSelectedEvent: (event: CalendarEvent | undefined) => (currentState: MonthCalendarState) => MonthCalendarState;
    setSelectedEvents: (events: CalendarEvent[]) => (currentState: MonthCalendarState) => MonthCalendarState;
    setCurrentEvents: (events: CalendarEvent[]) => (currentState: MonthCalendarState) => MonthCalendarState;
    toggleEventOverlay: (visible: boolean) => (currentState: MonthCalendarState) => MonthCalendarState;
    toggleEventSelector: (visible: boolean) => (currentState: MonthCalendarState) => MonthCalendarState;
};
