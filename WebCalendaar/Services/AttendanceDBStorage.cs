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
        Attendance? attendanceInDatabase = await db.Attendance.FirstOrDefaultAsync(a => a.AttendanceId == attendance.AttendanceId);
        if (attendanceInDatabase != null)
            return false;

        await db.Attendance.AddAsync(attendance);

        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<bool> Delete(int attendanceId)
    {
        Attendance? attendanceInDatabase = await db.Attendance.FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);
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
        Attendance? attendanceInDatabase = await db.Attendance.FirstOrDefaultAsync(a => a.AttendanceId == attendance.AttendanceId);
        if (attendanceInDatabase == null)
            return false;
        
        attendanceInDatabase.AttendanceDate = attendance.AttendanceDate;
        attendanceInDatabase.AttendanceId = attendance.AttendanceId;
        attendanceInDatabase.UserId = attendance.UserId;
        
        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<Attendance?> Find(int attendanceId)
    {
        Attendance? attendanceInDatabase = await db.Attendance.FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);
        if (attendanceInDatabase == null)
            return null;
        
        return attendanceInDatabase;        
    }

    public async Task<List<Attendance>> FindMany(int[] attendanceIds)
    {
        List<Attendance> attendanceInDatabase = await db.Attendance.Where(A => attendanceIds.Contains(A.AttendanceId)).ToListAsync();
        return attendanceInDatabase;
    }    
}