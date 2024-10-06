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
    public async Task<IActionResult> CreateAttendance(Guid eventId, int userId, string feedback) {
        User? currentUser = await userStorage.Read(userId);
        if (currentUser == null) return NotFound("User not found");

        Event? pickedEvent = await eventStorage.Find(_ => _.id == eventId);
        if (pickedEvent == null) return NotFound("Event not found");

        Event_Attendance eventAttendance = new() {Feedback = feedback, User = currentUser, Event = pickedEvent};
        
        return Created();
    }
}