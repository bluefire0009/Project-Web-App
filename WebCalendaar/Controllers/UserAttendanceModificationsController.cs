using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;

[Route("api/Attendance")]
[RequiresUserLogin]
public class UserAttendanceModificationsController : Controller
{
    readonly IAttendanceStorage attendanceStorage;

    public UserAttendanceModificationsController(IAttendanceStorage attendanceStorage)
    {
        this.attendanceStorage = attendanceStorage;
    }

    [HttpPut("Change")]
    public async Task<IActionResult> ModifyAttendance()
    {
        string myEmail = this.HttpContext.Session.GetString("LoggedInUser");
        //List<Attendance> myAttandaces = (await attendanceStorage.GetAll()).Where(a => a.UserId == myEmail).ToList();
        
        return Ok();
    }
}