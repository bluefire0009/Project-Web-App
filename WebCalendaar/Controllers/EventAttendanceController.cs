using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;

[Route("api/eventAttendance")]
public class EventAttendanceController : Controller {
    readonly IEventAttendanceStorage Storage;

    public EventAttendanceController(IEventAttendanceStorage storage) {
        Storage = storage;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateEventAttendance([FromBody] Event_Attendance eventAttendance) {
        var result = Storage.Create(eventAttendance);
        return Created(eventAttendance.Event_AttendanceId.ToString(), result);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetEventAttendance([FromQuery] int id) {
        if (await Storage.Find(id) == null) return NoContent();
        var found = await Storage.Find(id);
        return Ok(found);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateEventAttendance([FromBody] Event_Attendance eventAttendance) {
        await Storage.Update(eventAttendance);
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteEventAttendance([FromQuery] int id) {
        await Storage.Delete(id);
        return Ok();
    }
}
