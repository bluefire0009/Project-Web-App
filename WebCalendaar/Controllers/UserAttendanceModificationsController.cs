using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebCalendaar.Models;

[Route("api/Attendance")]
[RequiresUserLogin]
public class UserAttendanceModificationsController : Controller
{
    readonly IAttendanceStorage attendanceStorage;
    readonly IEventStorage eventStorage;

    public UserAttendanceModificationsController(IAttendanceStorage attendanceStorage, IEventStorage eventStorage)
    {
        this.attendanceStorage = attendanceStorage;
        this.eventStorage = eventStorage;
    }

    [HttpPut("Change")]
    public async Task<IActionResult> ModifyAttendance([FromQuery] DateOnly dateFrom, [FromQuery] DateOnly dateTo)
    {
        // Cannot be null because the controller checks if user is logged in, and if user is logged in, they should have an id
        int myUserId = int.Parse(HttpContext.Session.GetString("LoggedInUser"));
        List<Attendance> myAttandaces = (await attendanceStorage.GetAll()).Where(a => a.UserId == myUserId).ToList();

        // Check if the user actually has an attendance on dateFrom
        bool allowedDateFrom = myAttandaces.Where(a => a.AttendanceDate == dateFrom).FirstOrDefault() != null;
        if (!allowedDateFrom) return BadRequest("User doesn't have an attendance on the dateFrom");

        // Check if the there is an event on dateTo
        bool allowedDateTo = (await eventStorage.GetAll()).Select(e => e.EventDate).Contains(dateTo);
        if (!allowedDateTo) return BadRequest("No event on dateTo");

        // Change Date
        Attendance requestAttendance = myAttandaces.Where(a => a.AttendanceDate == dateFrom).FirstOrDefault();
        Attendance updatedAttendance = new() { AttendanceDate = dateTo, UserId = requestAttendance.UserId };
        bool updated = await attendanceStorage.Update(updatedAttendance, requestAttendance.UserId, requestAttendance.AttendanceDate);

        if (updated) return Ok($"Updated the database with: {JsonConvert.SerializeObject(updatedAttendance)}");
        return BadRequest("Couldn't update the database");
    }
}