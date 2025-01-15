using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;

// Controller to manage event attendance operations
[Route("api/eventAttendance")]
public class EventAttendanceController : Controller
{
    // Dependency for event attendance storage operations
    readonly IEventAttendanceStorage Storage;

    // Constructor to inject the event attendance storage dependency
    public EventAttendanceController(IEventAttendanceStorage storage)
    {
        Storage = storage;
    }

    // Creates a new event attendance record
    [HttpPost("Post")]
    public async Task<IActionResult> CreateEventAttendance([FromBody] Event_Attendance eventAttendance)
    {
        if (eventAttendance == null)
        {
            // Return 400 if the input event attendance is null
            return BadRequest("Event attendance cannot be null");
        }

        // Save the event attendance to storage
        await Storage.Create(eventAttendance);
        // Return 201 with the created event attendance details
        return CreatedAtAction(nameof(GetEventAttendance), new { id = eventAttendance.Event_AttendanceId }, eventAttendance);
    }

    // Retrieves a specific event attendance by ID
    [HttpGet("Get")]
    public async Task<IActionResult> GetEventAttendance([FromQuery] int id)
    {
        var attendance = await Storage.Find(id); // Find the event attendance by ID
        if (attendance == null)
        {
            // Return 404 if the event attendance is not found
            return NotFound($"Event attendance with id {id} not found");
        }
        return Ok(attendance); // Return the event attendance details
    }

    // Updates an existing event attendance record
    [HttpPut("Put")]
    public async Task<IActionResult> UpdateEventAttendance([FromBody] Event_Attendance eventAttendance)
    {
        if (eventAttendance == null)
        {
            // Return 400 if the input event attendance is null
            return BadRequest("Event attendance cannot be null");
        }

        // Update the event attendance in storage
        await Storage.Update(eventAttendance);
        return Ok(eventAttendance); // Return the updated event attendance
    }

    // Deletes an event attendance record by ID
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteEventAttendance([FromQuery] int id)
    {
        var existing = await Storage.Find(id); // Find the event attendance by ID
        if (existing == null)
        {
            // Return 404 if the event attendance is not found
            return NotFound($"Event attendance with id {id} not found");
        }

        // Delete the event attendance from storage
        await Storage.Delete(id);
        // Return 200 if the deletion is successful
        return Ok($"Event attendance with id {id} deleted successfully");
    }
    
    // Retrieves all event attendance records
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllEventAttendances()
    {
        var attendances = await Storage.GetAll(); // Fetch all event attendances
        if (attendances == null || !attendances.Any())
        {
            // Return 404 if no event attendances are found
            return NotFound("No event attendances found");
        }
        return Ok(attendances); // Return the list of event attendances
    }

    // Retrieves all event attendance records for a specific event
    [HttpGet("GetByEvent/{eventId}")]
    public async Task<IActionResult> GetEventAttendancesByEvent(int eventId)
    {
        var attendances = await Storage.GetAllByEventId(eventId); // Fetch attendances by event ID

        if (attendances == null || !attendances.Any())
        {
            // Return 404 if no attendances are found for the event
            return NotFound($"No attendances found for event with ID {eventId}");
        }

        return Ok(attendances); // Return the list of event attendances for the event
    }
};
