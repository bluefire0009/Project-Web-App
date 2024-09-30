using WebCalendaar.Models;

public class InMemoryAttendanceStorage : IAttendanceStorage
{
    public static List<Attendance> attendances = new();

    public async Task Create(Attendance attendance)
    {
        attendance.AttendanceId = Guid.NewGuid();
        attendance.User.UserId = Guid.NewGuid();
        await Task.Delay(0);
        attendances.Add(attendance);
    }

    public async Task Delete(Guid attendanceId)
    {
        await Task.Delay(0);
        attendances.Remove(attendances.Find(a => a.AttendanceId == attendanceId));
    }

    public async Task<Attendance?> Find(Guid attendanceId)
    {
        await Task.Delay(0);
        return attendances.Find(a => a.AttendanceId == attendanceId);
    }

    public async Task<List<Attendance>> FindMany(Guid[] attendanceIds)
    {
        List<Attendance> found = new();
        await Task.Delay(0);

        attendances.ForEach(a => 
        {
            attendanceIds.ToList().ForEach(id =>
            {
                if (a.AttendanceId == id)
                    found.Add(a);
            });
        });
        return found;
    }
}