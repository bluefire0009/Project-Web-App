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
        // Check if User from the attendance exsists in the database
        User? userInDatabase = await db.User.FirstOrDefaultAsync(u => u.UserId == attendance.UserId);
        if (userInDatabase == null) return false;

        Attendance? attendanceInDatabase = await db.Attendance.Where(a => a.UserId == attendance.UserId && a.AttendanceDate == attendance.AttendanceDate).FirstOrDefaultAsync();
        if (attendanceInDatabase != null)
            return false;

        await db.Attendance.AddAsync(attendance);

        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<bool> Delete(int userId, DateOnly attendanceDate)
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

    // attendance is the object that will be changed to in the database
    public async Task<bool> Update(Attendance attendance, int userIdToUpdate, DateOnly dateToUpdate)
    {
        Attendance? attendanceInDatabase = await db.Attendance.FirstOrDefaultAsync(a => a.UserId == attendance.UserId && a.AttendanceDate == dateToUpdate);
        bool sameFields = attendance.AttendanceDate == dateToUpdate && attendance.UserId == userIdToUpdate;
        if (attendanceInDatabase == null || sameFields)
            return false;

        // This has to be done because the attandance is a composite key
        db.Attendance.Remove(attendanceInDatabase);
        await db.SaveChangesAsync();

        attendanceInDatabase.AttendanceDate = attendance.AttendanceDate;
        attendanceInDatabase.UserId = attendance.UserId;
        db.Attendance.Add(attendanceInDatabase);

        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<Attendance?> Find(int userId, DateOnly attendanceDate)
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

    public async Task<List<Attendance>> GetAll()
    {
        return db.Attendance.ToList();
    }

    public async Task<List<Attendance>> GetAllUpcomingByUser(int UserId)
    {
        List<Attendance> data = db.Attendance.Where(_ => _.UserId == UserId).ToList();
        return data.Where(_ => _.AttendanceDate > DateOnly.FromDateTime(DateTime.Now)).ToList();
    }

    public async Task<bool> LeaveReview(int eventId, int myUserId, string rating, string review)
    {
        //if (rating < 0 || rating > 5) return false;

        Event_Attendance? myAttendance = await db.Event_Attendance.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == myUserId);
        if (myAttendance == null) return false;

        myAttendance.Feedback = review;
        myAttendance.Rating = rating;
        // This looks scuffed because the intended way of DateOnly doesnt work for some reason
        // So in the model datePlaced is a string, and here I will get the current DateTime
        // And then use ToString so it will work in the database
        myAttendance.DatePlaced = DateOnly.FromDateTime(DateTime.Today).ToString();

        int nrChhanges = db.SaveChanges();

        if (nrChhanges > 0) return true;
        return false;
    }
}