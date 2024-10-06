using WebCalendaar.Models;

public interface IEventAttendanceStorage {
    Task<bool> Create(Event_Attendance eventAttendance);
    Task<Event_Attendance?> Find(Guid id);
    Task Update(Event_Attendance eventAttendance);
    Task Delete(Guid id);
}

public class EventAttendanceDBStorage : IEventAttendanceStorage {
    DatabaseContext Db;

    public EventAttendanceDBStorage(DatabaseContext db) {
        Db = db;
    }

    // Creates an entry of event_attendance
    public async Task<bool> Create(Event_Attendance eventAttendance)
    {
        Event_Attendance? existing = await Db.Event_Attendance.FirstOrDefaultAsync(_ => _.Id == employee.Id);
        if (existing is not null) return false;
        await Db.Event_Attendance.AddAsync(eventAttendance);
        int n = await Db.SaveChangesAsync();
        return n>0;
    }

    // Deletes an entry of event_attendance
    public async Task<bool> Delete(Guid id)
    {
        Event_Attendance? x = await Db.Event_Attendance.FirstOrDefaultAsync<Event_Attendance>(_=>_.Id==id);
        if(x==null) return false;
        Db.Event_Attendance.Remove(x);
        int n = await Db.SaveChangesAsync();
        return n>0;
    }

    // Finds an entry of event_attendance by id
    public async Task<Event_Attendance?> Find(Guid id)
    {
        return await Db.Event_Attendance.FirstOrDefaultAsync<Event_Attendance>(_ => _.Id == id);
    }

    // Updates an entry of event_Attendance by the id in the object
    public async Task<bool> Update(Event_Attendance eventAttendance)
    {
        Db.Event_Attendance.Update(eventAttendance);
        int n = await Db.SaveChangesAsync();
        return n>0;
    }
}