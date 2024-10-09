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
        if (attendance == null){
            return BadRequest("Null in the request");
        }
        await attendanceStorage.Create(attendance);
        return Created($"Created Attendance: ", attendance);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetAttendance([FromQuery] int id)
    {
        if (attendanceStorage.Find(id).Result == null){
            return NotFound($"Id : {id} not in the database");
        }
        Attendance found = await attendanceStorage.Find(id);
        return Ok(found);
    }

    [HttpPut("Put")]
    public async Task<IActionResult> UpdateAttendance([FromBody] Attendance attendance)
    {
        if (attendance == null){
            return BadRequest("Null in the request");
        } else if (attendanceStorage.Find(attendance.AttendanceId).Result == null){
            return NotFound($"Id : {attendance.AttendanceId} not in the database");
        }

        await attendanceStorage.Update(attendance);

        return Created($"Updated Attendance with Id={attendance.AttendanceId} to: ", attendance);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAttendance([FromQuery] int idToDelete)
    {
        if (attendanceStorage.Find(idToDelete).Result == null){
            return NotFound($"Id : {idToDelete} not in the database");
        }
        await attendanceStorage.Delete(idToDelete);
        return Ok($"Deleted the Attendance with Id={idToDelete}");
    }
}