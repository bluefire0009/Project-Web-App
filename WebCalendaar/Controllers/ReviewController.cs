using WebCalendaar.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/Attendance")]
public class ReviewController : Controller
{
    readonly IAttendanceStorage attendanceStorage;

    public ReviewController(IAttendanceStorage attendanceStorage)
    {
        this.attendanceStorage = attendanceStorage;
    }

    [RequiresUserLogin]
    [HttpPost("Review")]
    public async Task<IActionResult> LeaveAReview([FromQuery] int eventId, [FromQuery] int rating, [FromQuery] string review)
    {
        int myUserId = int.Parse(HttpContext.Session.GetString("LoggedInUser"));
        bool added = await attendanceStorage.LeaveReview(eventId, myUserId, rating, review);
        return Ok();
    }

    [RequiresAdminLogin]
    [HttpDelete("Review")]
    public async Task<IActionResult> DeleteAReview([FromQuery] int eventId, [FromQuery] int userId)
    {
        bool added = await attendanceStorage.LeaveReview(eventId, userId, 0, "");
        return Ok();
    }
}