using WebCalendaar.Models;

public class InMemoryAttendanceStorage : IAttendanceStorage
{
    public static List<Attendance> attendances = new();

    public async Task<bool> Create(Attendance attendance)
    {
        await Task.Delay(0);
        attendances.Add(attendance);
        return true;
    }

    public async Task<bool> Delete(int attendanceId)
    {
        await Task.Delay(0);
        attendances.Remove(attendances.Find(a => a.AttendanceId == attendanceId));
        return true;
    }

    public async Task<Attendance?> Find(int attendanceId)
    {
        await Task.Delay(0);
        return attendances.Find(a => a.AttendanceId == attendanceId);
    }

    public async Task<List<Attendance>> FindMany(int[] attendanceIds)
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

    // this was added later when not needed anymore
    public Task<bool> Update(Attendance attendance)
    {
        throw new NotImplementedException();
    }
}