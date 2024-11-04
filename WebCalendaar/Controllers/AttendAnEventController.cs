using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;

[Route("api/AttendAnEvent")]
public class AttendAnEventController : Controller {
    public IUserStorage userStorage;
    public IEventStorage eventStorage;

    public AttendAnEventController(IUserStorage userStorage, IEventStorage eventStorage)
    {
        this.userStorage = userStorage;
        this.eventStorage = eventStorage;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateAttendance([FromQuery] int eventId, [FromQuery] int userId, [FromQuery] string feedback = "") {
        User? currentUser = await userStorage.Read(userId);
        Event? pickedEvent = await eventStorage.Read(eventId);

        if (currentUser == null && pickedEvent == null) return NotFound("User and Event not found");
        else if (currentUser == null) return NotFound("User not found");
        else if (pickedEvent == null) return NotFound("Event not found");

        Event_Attendance eventAttendance = new() {Feedback = feedback, User = currentUser, Event = pickedEvent};
        
        return Created();
    }
}