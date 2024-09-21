using Microsoft.AspNetCore.Mvc;

[Route("api/v1/eventAttendance")]
public class EventAttendanceController : Controller {
    readonly IEventAttendanceStorage Storage;

    public EventAttendanceController(IEventAttendanceStorage storage) {
        Storage = storage;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateEventAttendance([FromBody] EventAttendance eventAttendance) {
        return Created(eventAttendance.Id.ToString(), Storage.Create(eventAttendance));
    }

    [HttpGet()]
    public async Task<IActionResult> GetEventAttendance([FromQuery] Guid id) {
        return Ok(Storage.Find(id));
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateEventAttendance([FromBody] EventAttendance eventAttendance) {
        await Storage.Update(eventAttendance);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEventAttendance([FromQuery] Guid id) {
        await Storage.Delete(id);
        return Ok();
    }
}

//CRUD