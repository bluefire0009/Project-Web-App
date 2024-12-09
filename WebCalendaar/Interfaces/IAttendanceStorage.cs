using WebCalendaar.Models;

public interface IAttendanceStorage
{
    Task<bool> Create(Attendance attendance);
    Task<bool> Delete(int userId, DateOnly attendanceDate);
    Task<bool> Update(Attendance attendance, int userIdToUpdate, DateOnly attendanceDateToUpdate);
    Task<Attendance?> Find(int userId, DateOnly attendanceDate);
    Task<bool> IdExists(int userId);
    Task<List<Attendance>> GetAllForUser(int userId); // Specific to UserAttendanceModificationsController
}