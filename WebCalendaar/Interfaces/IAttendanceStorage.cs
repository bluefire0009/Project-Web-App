using WebCalendaar.Models;

public interface IAttendanceStorage
{
    Task<bool> Create(Attendance attendance);
    Task<bool> Delete(int attendanceId);
    Task<bool> Update(Attendance attendance);
    Task<Attendance?> Find(int attendanceId);
    Task<List<Attendance>> FindMany(int[] attendanceIds);
}