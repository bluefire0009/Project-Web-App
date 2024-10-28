using WebCalendaar.Models;

public interface IAttendanceStorage
{
    Task<bool> Create(Attendance attendance);
    Task<bool> Delete(int userId, DateTime attendanceDate);
    Task<bool> Update(Attendance attendance);
    Task<Attendance?> Find(int userId, DateTime attendanceDate);
    Task<bool> IdExsists(int userId);
}