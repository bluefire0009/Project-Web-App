using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;

[Route("api/eventAttendance")]
public class EventAttendanceController : Controller {
    readonly IEventAttendanceStorage EventAttendanceStorage;
    readonly IUserStorage UserStorage;
    readonly IEventStorage EventStorage;

    public EventAttendanceController(IEventAttendanceStorage eventAttendanceStorage, IUserStorage userStorage, IEventStorage eventStorage) {
        EventAttendanceStorage = eventAttendanceStorage;
        UserStorage = userStorage;
        EventStorage = eventStorage;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> CreateEventAttendance([FromBody] Event_Attendance eventAttendance) {
        if (eventAttendance == null) {
            return BadRequest("Event attendance cannot be null");
        }

        Event? existingEvent = await EventStorage.Read(eventAttendance.EventId);
        User? existingUser = await UserStorage.Read(eventAttendance.UserId);

        if (existingEvent == null && existingUser == null) { 
            return BadRequest($"No event with id:{eventAttendance.EventId} found and no user with id:{eventAttendance.UserId} found");
        } else if (existingEvent == null) {
            return BadRequest($"No event with id:{eventAttendance.EventId} found");
        } else if (existingUser == null) {
            return BadRequest($"No user with id:{eventAttendance.UserId} found");
        }

        Event_Attendance? existingEventAttendance = await EventAttendanceStorage.Find(eventAttendance.Event_AttendanceId);
        if (existingEventAttendance != null) {
            return BadRequest($"Event_Attendance with given id:{eventAttendance.Event_AttendanceId} already exists");
        }

        await EventAttendanceStorage.Create(eventAttendance);
        return CreatedAtAction(nameof(GetEventAttendance), new { id = eventAttendance.Event_AttendanceId }, eventAttendance);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetEventAttendance([FromQuery] int id) {
        var attendance = await EventAttendanceStorage.Find(id);
        if (attendance == null) {
            return NotFound($"Event attendance with id {id} not found");
        }
        return Ok(attendance);
    }

    [HttpPut("Put")]
    public async Task<IActionResult> UpdateEventAttendance([FromBody] Event_Attendance eventAttendance) {
        if (eventAttendance == null) {
            return BadRequest("Event attendance cannot be null");
        }

        await EventAttendanceStorage.Update(eventAttendance);
        return Ok(eventAttendance);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteEventAttendance([FromQuery] int id) {
        var existing = await EventAttendanceStorage.Find(id);
        if (existing == null) {
            return NotFound($"Event attendance with id {id} not found");
        }

        await EventAttendanceStorage.Delete(id);
        return Ok($"Event attendance with id {id} deleted successfully");
    }
};