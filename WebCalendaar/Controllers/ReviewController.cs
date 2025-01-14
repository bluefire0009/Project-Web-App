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

    //[RequiresUserLogin]
    [HttpPost("Review")]
    public async Task<IActionResult> LeaveAReview([FromQuery] int eventId, [FromQuery] string rating, [FromQuery] string review)
    {
        int myUserId = int.Parse(HttpContext.Session.GetString("LoggedInUser"));
        bool added = await attendanceStorage.LeaveReview(eventId, myUserId, rating, review);
        if (added) return Created("", $"Left a review for event with id: {eventId} rating: {rating} and message: {review}");
        return BadRequest("Something went wrong or this exact review already exsists");
    }

    [RequiresAdminLogin]
    [HttpDelete("Review")]
    public async Task<IActionResult> DeleteAReview([FromQuery] int eventId, [FromQuery] int userId)
    {
        bool removed = await attendanceStorage.LeaveReview(eventId, userId, "", "");
        if (removed) return Ok($"Removed the review from an event id: {eventId} of the user {userId}");
        return BadRequest("Something went wrong");
    }
}