using Microsoft.EntityFrameworkCore;
using WebCalendaar.Models;

public class AttendanceDBStorage : IAttendanceStorage
{
    private DatabaseContext db;

    public AttendanceDBStorage(DatabaseContext db)
    {
        this.db = db;
    }

    public async Task<bool> Create(Attendance attendance)
    {
        Attendance? attendanceInDatabase = await db.Attendance.FirstOrDefaultAsync(a => a.UserId == attendance.UserId && a.AttendanceDate == attendance.AttendanceDate);
        if (attendanceInDatabase != null)
            return false;

        await db.Attendance.AddAsync(attendance);

        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<bool> Delete(int userId, DateTime attendanceDate)
    {
        Attendance? attendanceInDatabase = await db.Attendance.FirstOrDefaultAsync(a => a.UserId == userId && a.AttendanceDate == attendanceDate);
        if (attendanceInDatabase == null)
            return false;

        db.Attendance.Remove(attendanceInDatabase);

        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<bool> Update(Attendance attendance)
    {
        Attendance? attendanceInDatabase = await db.Attendance.FirstOrDefaultAsync(a => a.UserId == attendance.UserId && a.AttendanceDate == attendance.AttendanceDate);
        if (attendanceInDatabase == null)
            return false;

        attendanceInDatabase.AttendanceDate = attendance.AttendanceDate;
        attendanceInDatabase.UserId = attendance.UserId;

        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<Attendance?> Find(int userId, DateTime attendanceDate)
    {
        Attendance? attendanceInDatabase = await db.Attendance.FirstOrDefaultAsync(a => a.UserId == userId && a.AttendanceDate == attendanceDate);
        if (attendanceInDatabase == null)
            return null;

        return attendanceInDatabase;
    }

    public async Task<bool> IdExsists(int userId)
    {
        Attendance? found = await db.Attendance.FirstOrDefaultAsync(a => a.UserId == userId);
        return found != null;
    }
}