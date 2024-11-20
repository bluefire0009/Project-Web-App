using WebCalendaar.Models;

public interface IAttendanceStorage
{
    Task<bool> Create(Attendance attendance);
    Task<bool> Delete(int userId, DateOnly attendanceDate);
    Task<bool> Update(Attendance attendance, int userIdToUpdate, DateOnly attendanceDateToUpdate);
    Task<Attendance?> Find(int userId, DateOnly attendanceDate);
    Task<bool> IdExsists(int userId);
    Task<List<Attendance>> GetAll();
    Task<bool> LeaveReview(int eventId, int myUserId, int rating, string review);
}