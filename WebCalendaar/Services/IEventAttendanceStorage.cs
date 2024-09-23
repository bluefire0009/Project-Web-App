using WebCalendaar.Models;

public interface IEventAttendanceStorage {
    Task Create(Event_Attendance eventAttendance);
    Task<Event_Attendance?> Find(Guid id);
    Task Update(Event_Attendance eventAttendance);
    Task Delete(Guid id);
}