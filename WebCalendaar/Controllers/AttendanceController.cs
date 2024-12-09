using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebCalendaar.Models;

[Route("api/Attendance")]
[ApiController]
public class AttendanceController : Controller
{
    private readonly IAttendanceStorage attendanceStorage;
    private readonly IEventAttendanceStorage eventAttendanceStorage;
    private readonly IEventStorage eventStorage;
    private readonly IUserStorage userStorage;

    public AttendanceController(
        IAttendanceStorage attendanceStorage,
        IEventAttendanceStorage eventAttendanceStorage,
        IEventStorage eventStorage,
        IUserStorage userStorage)
    {
        this.attendanceStorage = attendanceStorage;
        this.eventAttendanceStorage = eventAttendanceStorage;
        this.eventStorage = eventStorage;
        this.userStorage = userStorage;
    }

    // POST: Create attendance for a user (replaces Add and CreateAttendance)
    [HttpPost("Create")]
    public async Task<IActionResult> CreateAttendance(
        [FromQuery] int eventId,
        [FromQuery] int userId,
        [FromQuery] DateOnly attendanceDate,
        [FromQuery] string feedback = "")
    {
        var user = await userStorage.Read(userId);
        var eventDetails = await eventStorage.Read(eventId);

        if (user == null || eventDetails == null)
        {
            return NotFound(user == null ? "User not found" : "Event not found");
        }

        var existingAttendance = await eventAttendanceStorage.FindByUserAndEvent(userId, eventId);
        if (existingAttendance != null)
        {
            return BadRequest("Attendance already exists for this user and event.");
        }

        var eventAttendance = new Event_Attendance
        {
            User = user,
            Event = eventDetails,
            Feedback = feedback
        };

        var attendance = new Attendance
        {
            UserId = userId,
            AttendanceDate = attendanceDate
        };

        var attendanceAdded = await attendanceStorage.Create(attendance);
        var eventAttendanceAdded = await eventAttendanceStorage.Create(eventAttendance);

        if (attendanceAdded && eventAttendanceAdded)
        {
            return Created($"Attendance created for User: {userId}, Event: {eventId}", eventAttendance);
        }

        return BadRequest("Failed to create attendance.");
    }

    // GET: Fetch attendance details for a user or event
    [HttpGet("Details")]
    public async Task<IActionResult> GetAttendanceDetails([FromQuery] int userId, [FromQuery] DateOnly? date, [FromQuery] int? eventId)
    {
        if (eventId.HasValue)
        {
            var eventAttendanceList = await eventAttendanceStorage.GetAllForEvent(eventId.Value);
            return Ok(eventAttendanceList);
        }

        if (date.HasValue)
        {
            var attendance = await attendanceStorage.Find(userId, date.Value);
            return attendance == null
                ? NotFound("Attendance not found.")
                : Ok(attendance);
        }

        var userAttendance = await attendanceStorage.GetAllForUser(userId);
        return Ok(userAttendance);
    }

    // PUT: Update user attendance date (replaces ModifyAttendance)
    [HttpPut("ChangeDate")]
    public async Task<IActionResult> ModifyAttendanceDate([FromQuery] int userId, [FromQuery] DateOnly dateFrom, [FromQuery] DateOnly dateTo)
    {
        var attendance = await attendanceStorage.Find(userId, dateFrom);
        if (attendance == null)
        {
            return BadRequest("No attendance found for the specified date.");
        }

        var updatedAttendance = new Attendance
        {
            UserId = userId,
            AttendanceDate = dateTo
        };

        var result = await attendanceStorage.Update(updatedAttendance, userId, dateFrom);
        return result
            ? Ok($"Attendance date updated from {dateFrom} to {dateTo}.")
            : BadRequest("Failed to update attendance.");
    }

    // PUT: Update event feedback (updates event attendance)
    [HttpPut("Feedback")]
    public async Task<IActionResult> UpdateFeedback([FromQuery] int eventId, [FromQuery] int userId, [FromBody] string feedback)
    {
        var existing = await eventAttendanceStorage.FindByUserAndEvent(userId, eventId);
        if (existing == null)
        {
            return NotFound("No event attendance found for the given user and event.");
        }

        existing.Feedback = feedback;
        var updated = await eventAttendanceStorage.Update(existing);
        return updated
            ? Ok("Feedback updated successfully.")
            : BadRequest("Failed to update feedback.");
    }

    // DELETE: Remove attendance (user or event-specific)
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAttendance([FromQuery] int userId, [FromQuery] DateOnly? date, [FromQuery] int? eventId)
    {
        if (eventId.HasValue)
        {
            var existing = await eventAttendanceStorage.FindByUserAndEvent(userId, eventId.Value);
            if (existing == null)
            {
                return NotFound("Event attendance not found.");
            }

            var deleted = await eventAttendanceStorage.Delete(existing.Event_AttendanceId);
            return deleted
                ? Ok("Event attendance deleted successfully.")
                : BadRequest("Failed to delete event attendance.");
        }

        if (!date.HasValue)
        {
            return BadRequest("Attendance date is required for user attendance deletion.");
        }

        var result = await attendanceStorage.Delete(userId, date.Value);
        return result
            ? Ok("Attendance deleted successfully.")
            : BadRequest("Failed to delete attendance.");
    }
}