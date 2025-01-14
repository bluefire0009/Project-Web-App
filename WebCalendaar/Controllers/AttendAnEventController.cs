using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;

[Route("api/AttendAnEvent")]
public class AttendAnEventController : Controller
{
    public IUserStorage userStorage;
    public IEventStorage eventStorage;
    public IEventAttendanceStorage eventAttendanceStorage;

    public AttendAnEventController(IUserStorage userStorage, IEventStorage eventStorage, IEventAttendanceStorage eventAttendanceStorage)
    {
        this.userStorage = userStorage;
        this.eventStorage = eventStorage;
        this.eventAttendanceStorage = eventAttendanceStorage;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateAttendance([FromQuery] int eventId, [FromQuery] int userId)
    {
        User? currentUser = await userStorage.Read(userId);
        Event? pickedEvent = await eventStorage.Read(eventId);

        if (currentUser == null && pickedEvent == null) return NotFound("User and Event not found");
        else if (currentUser == null) return NotFound("User not found");
        else if (pickedEvent == null) return NotFound("Event not found");

        Event_Attendance eventAttendance = new() { User = currentUser, UserId = userId, Feedback = "", Rating = "", Event = pickedEvent, EventId = eventId };
        bool added = await eventAttendanceStorage.Create(eventAttendance);
        if (added == false) return BadRequest("Something went wrong");
        return Created("Created:", await eventAttendanceStorage.FindByUserComposite(userId, eventId));
    }
}