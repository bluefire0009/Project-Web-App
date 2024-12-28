export type CalendarEvent = {
    EventId: bigint;
    Title: string;
    Description: string;
    StartTime: Date;
    EndTime: Date;
    Location: string;
};

export type CalendarState = {
    currentDate: Date;
    currentMonth: Date[];
    currentEvents: CalendarEvent[];
    selectedEvent: CalendarEvent | undefined;
    isEventOverlayVisible: boolean;
    setCurrentDate: (date: Date) => (currentState: CalendarState) => CalendarState;
    setCurrentMonth: (date: Date) => (currentState: CalendarState) => CalendarState;
    setSelectedEvent: (event: CalendarEvent | undefined) => (currentState: CalendarState) => CalendarState;
    toggleEventOverlay: (visible: boolean) => (currentState: CalendarState) => CalendarState;
};
