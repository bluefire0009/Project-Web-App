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
        await attendanceStorage.Create(attendance);
        return Created($"Created Attendance: \n", attendance);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetAttendance([FromQuery] Guid id)
    {
        Attendance found = await attendanceStorage.Find(id);
        return Ok(found);
    }

    [HttpPut("Put")]
    public async Task<IActionResult> UpdateAttendance([FromBody] Attendance attendance,[FromQuery] Guid idToUpdate)
    {
        await attendanceStorage.Delete(idToUpdate);
        await attendanceStorage.Create(attendance);
        return Created($"Updated Attendance with Id={idToUpdate} to: \n", attendance);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAttendance([FromQuery] Guid idToDelete)
    {
        await attendanceStorage.Delete(idToDelete);
        return Ok($"Deleted the Attendance with Id={idToDelete}");
    }
}