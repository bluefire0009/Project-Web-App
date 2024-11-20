using WebCalendaar.Models;
using Microsoft.EntityFrameworkCore;

public interface IEventAttendanceStorage
{
    Task<bool> Create(Event_Attendance eventAttendance);
    Task<Event_Attendance?> Find(int id);
    Task<Event_Attendance?> FindByUserComposite(int userId, int eventId);
    Task<bool> Update(Event_Attendance eventAttendance);
    Task<bool> Delete(int id);

}

public class EventAttendanceDBStorage : IEventAttendanceStorage
{
    DatabaseContext Db;

    public EventAttendanceDBStorage(DatabaseContext db)
    {
        Db = db;
    }

    // Creates an entry of event_attendance
    public async Task<bool> Create(Event_Attendance eventAttendance)
    {
        Event_Attendance? existing = await Db.Event_Attendance.FirstOrDefaultAsync(_ => _.User == eventAttendance.User);
        if (existing is not null) return false;
        await Db.Event_Attendance.AddAsync(eventAttendance);
        int n = await Db.SaveChangesAsync();
        return n > 0;
    }

    // Deletes an entry of event_attendance
    public async Task<bool> Delete(int id)
    {
        Event_Attendance? x = await Db.Event_Attendance.FirstOrDefaultAsync<Event_Attendance>(_ => _.Event_AttendanceId == id);
        if (x == null) return false;
        Db.Event_Attendance.Remove(x);
        int n = await Db.SaveChangesAsync();
        return n > 0;
    }

    // Finds an entry of event_attendance by id
    public async Task<Event_Attendance?> Find(int id)
    {
        return await Db.Event_Attendance.FirstOrDefaultAsync<Event_Attendance>(_ => _.Event_AttendanceId == id);
    }

    // Finds an entry of event_attendance by user id
    public async Task<Event_Attendance?> FindByUserComposite(int userId, int eventId)
    {
        return await Db.Event_Attendance.FirstOrDefaultAsync(_ => _.UserId == userId && _.EventId == eventId);
    }

    // Updates an entry of event_Attendance by the id in the object
    public async Task<bool> Update(Event_Attendance eventAttendance)
    {
        Db.Event_Attendance.Update(eventAttendance);
        int n = await Db.SaveChangesAsync();
        return n > 0;
    }
}