using WebCalendaar.Models;

public interface IAttendanceStorage
{
    Task Create(Attendance attendance);
    Task Delete(Guid attendanceId);
    Task<Attendance?> Find(Guid attendanceId);
    Task<List<Attendance>> FindMany(Guid[] attendanceIds);
}