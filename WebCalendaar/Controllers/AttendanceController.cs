using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebCalendaar.Models;
[Route("api/Attendance")]
public class AttendanceController : Controller
{
    readonly IAttendanceStorage attendanceStorage;

    public AttendanceController(IAttendanceStorage attendanceStorage)
    {
        this.attendanceStorage = attendanceStorage;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddAttendance([FromBody] Attendance attendance)
    {
        if (attendance == null)
        {
            return BadRequest("Null in the request");
        }
        await attendanceStorage.Create(attendance);
        return Created($"Created Attendance: ", attendance);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetAttendance([FromQuery] int userId, [FromQuery] DateTime attendanceDate)
    {
        if (attendanceStorage.Find(userId, attendanceDate).Result == null)
        {
            return NotFound($"Combination : {userId},{attendanceDate} not in the database");
        }
        Attendance found = await attendanceStorage.Find(userId, attendanceDate);
        return Ok(found);
    }

    [HttpPut("Put")]
    public async Task<IActionResult> UpdateAttendance([FromBody] Attendance updatedAttendance, [FromQuery] int userIdToUpdate, [FromQuery] DateTime attendanceDateToUpdate)
    {
        // Check if the updated attandance has invalid content
        if (updatedAttendance == null || !attendanceStorage.IdExsists(updatedAttendance.UserId).Result)
        {
            return BadRequest("Invalid content in the attendance");
        }
        // check if the attendance is already in the database
        else if (attendanceStorage.Find(userIdToUpdate, attendanceDateToUpdate).Result == null)
        {
            return NotFound($"Combination : {userIdToUpdate},{attendanceDateToUpdate} not in the database");
        }

        await attendanceStorage.Update(updatedAttendance);

        return Created($"Updated Attendance with combination={userIdToUpdate},{attendanceDateToUpdate} to: ", updatedAttendance);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAttendance([FromQuery] int userId, [FromQuery] DateTime attendanceDate)
    {
        if (attendanceStorage.Find(userId, attendanceDate).Result == null)
        {
            return NotFound($"Combination : {userId},{attendanceDate} not in the database");
        }
        await attendanceStorage.Delete(userId, attendanceDate);
        return Ok($"Deleted the Attendance with combination={userId},{attendanceDate}");
    }
}