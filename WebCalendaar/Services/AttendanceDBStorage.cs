using Microsoft.EntityFrameworkCore;
using WebCalendaar.Models;

public class AttendanceDBStorage : IAttendanceStorage
{
    private readonly DatabaseContext db;

    public AttendanceDBStorage(DatabaseContext db)
    {
        this.db = db;
    }

    public async Task<bool> Create(Attendance attendance)
    {
        // Validate user existence
        var userInDatabase = await db.User.FirstOrDefaultAsync(u => u.UserId == attendance.UserId);
        if (userInDatabase == null) return false;

        // Check for duplicate attendance records
        var existingAttendance = await db.Attendance
            .FirstOrDefaultAsync(a => a.UserId == attendance.UserId && a.AttendanceDate == attendance.AttendanceDate);
        if (existingAttendance != null) return false;

        await db.Attendance.AddAsync(attendance);

        return await db.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int userId, DateOnly attendanceDate)
    {
        var attendance = await db.Attendance
            .FirstOrDefaultAsync(a => a.UserId == userId && a.AttendanceDate == attendanceDate);
        if (attendance == null) return false;

        db.Attendance.Remove(attendance);
        return await db.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Attendance attendance, int userIdToUpdate, DateOnly dateToUpdate)
    {
        // Find existing attendance record to update
        var attendanceInDatabase = await db.Attendance
            .FirstOrDefaultAsync(a => a.UserId == userIdToUpdate && a.AttendanceDate == dateToUpdate);

        if (attendanceInDatabase == null) return false;

        // Update composite key requires removing and re-adding the record
        db.Attendance.Remove(attendanceInDatabase);
        await db.SaveChangesAsync();

        attendanceInDatabase.UserId = attendance.UserId;
        attendanceInDatabase.AttendanceDate = attendance.AttendanceDate;

        await db.Attendance.AddAsync(attendanceInDatabase);

        return await db.SaveChangesAsync() > 0;
    }

    public async Task<Attendance?> Find(int userId, DateOnly attendanceDate)
    {
        return await db.Attendance
            .FirstOrDefaultAsync(a => a.UserId == userId && a.AttendanceDate == attendanceDate);
    }

    public async Task<bool> IdExists(int userId)
    {
        var found = await db.Attendance.FirstOrDefaultAsync(a => a.UserId == userId);
        return found != null;
    }

    public async Task<List<Attendance>> GetAllForUser(int userId)
    {
        return await db.Attendance
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Attendance>> GetAll()
    {
        return await db.Attendance.ToListAsync();
    }
}