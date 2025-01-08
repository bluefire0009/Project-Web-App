using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;

[Route("api/eventAttendance")]
public class EventAttendanceController : Controller {
    readonly IEventAttendanceStorage Storage;

    public EventAttendanceController(IEventAttendanceStorage storage) {
        Storage = storage;
    }

    [HttpPost("Post")]
    public async Task<IActionResult> CreateEventAttendance([FromBody] Event_Attendance eventAttendance) {
        if (eventAttendance == null) {
            return BadRequest("Event attendance cannot be null");
        }

        await Storage.Create(eventAttendance);
        return CreatedAtAction(nameof(GetEventAttendance), new { id = eventAttendance.Event_AttendanceId }, eventAttendance);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetEventAttendance([FromQuery] int id) {
        var attendance = await Storage.Find(id);
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

        await Storage.Update(eventAttendance);
        return Ok(eventAttendance);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteEventAttendance([FromQuery] int id) {
        var existing = await Storage.Find(id);
        if (existing == null) {
            return NotFound($"Event attendance with id {id} not found");
        }

        await Storage.Delete(id);
        return Ok($"Event attendance with id {id} deleted successfully");
    }
};