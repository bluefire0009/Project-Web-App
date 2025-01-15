using WebCalendaar.Models;
using Microsoft.AspNetCore.Mvc;

// Controller to manage reviews for event attendance
[Route("api/Attendance")]
public class ReviewController : Controller
{
    // Dependencies for attendance and user storage
    readonly IAttendanceStorage attendanceStorage;
    readonly IUserStorage userStorage;

    // Constructor to inject dependencies
    public ReviewController(IAttendanceStorage attendanceStorage, IUserStorage userStorage)
    {
        this.attendanceStorage = attendanceStorage;
        this.userStorage = userStorage;
    }

    //[RequiresUserLogin]
    [HttpPost("Review")]
    // Endpoint to leave a review for an event
    public async Task<IActionResult> LeaveAReview([FromQuery] int eventId, [FromQuery] string rating, [FromQuery] string review)
    {
        // Retrieve the user ID of the currently logged-in user from the session
        int myUserId = (await userStorage.ReadByEmail(HttpContext.Session.GetString("LoggedInUser"))).UserId;

        // Attempt to add the review for the event
        bool added = await attendanceStorage.LeaveReview(eventId, myUserId, rating, review);

        // Return appropriate response based on the result
        if (added)
            return Created("", $"Left a review for event with id: {eventId} rating: {rating} and message: {review}");
        
        return BadRequest("Something went wrong or this exact review already exists");
    }

    [RequiresAdminLogin]
    [HttpDelete("Review")]
    // Endpoint to delete a review for an event (admin only)
    public async Task<IActionResult> DeleteAReview([FromQuery] int eventId, [FromQuery] int userId)
    {
        // Attempt to remove the review by clearing its details
        bool removed = await attendanceStorage.LeaveReview(eventId, userId, "", "");

        // Return appropriate response based on the result
        if (removed)
            return Ok($"Removed the review from an event id: {eventId} of the user {userId}");
        
        return BadRequest("Something went wrong");
    }
}