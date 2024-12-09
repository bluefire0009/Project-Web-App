using WebCalendaar.Models;

public interface IEventAttendanceStorage
{
    Task<bool> Create(Event_Attendance eventAttendance);
    Task<Event_Attendance?> FindByUserAndEvent(int userId, int eventId); // Combined functionality
    Task<bool> Update(Event_Attendance eventAttendance);
    Task<bool> Delete(int id);
    Task<List<Event_Attendance>> GetAllForEvent(int eventId); // Specific to event management
}