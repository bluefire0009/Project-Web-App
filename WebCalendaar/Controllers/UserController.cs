using WebCalendaar.Models;
using WebCalendaar.Services;
using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Utils;

[Route("api/User")]
public class UserController : Controller
{
    private readonly ILoginService _loginService;
    private readonly IUserStorage _userStorage;
    private readonly IAttendanceStorage _AttendanceStorage;
    private readonly IEventAttendanceStorage _EventAttendanceStorage;
    private readonly IEventStorage _EventStorage;

    public UserController(ILoginService loginService, IUserStorage storage, IAttendanceStorage attendanceStorage, IEventAttendanceStorage eventAttendanceStorage, IEventStorage eventStorage)
    {
        this._userStorage = storage;
        this._loginService = loginService;
        this._AttendanceStorage = attendanceStorage;
        this._EventAttendanceStorage = eventAttendanceStorage;
        this._EventStorage = eventStorage;
    }

    [HttpPost("")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterBody registerBody)
    {
        // registerBody == Firstname, lastName, Email, Password
        RegisterStatus RegisterState = await _loginService.CheckIfCanRegisterUserAsync(registerBody);
        if (RegisterState == RegisterStatus.DuplicateEmail) 
            return BadRequest("This email is already registered");
        if (RegisterState == RegisterStatus.InvalidEmailFormat) 
            return BadRequest("This email has wrong format");
        if (RegisterState == RegisterStatus.InvalidPassword) 
            return BadRequest("Invalid password inputted");
        
        if (RegisterState == RegisterStatus.Success)
        {
            User newUser = new User
            {
                FirstName = registerBody.FirstName,
                LastName = registerBody.LastName,
                Email = registerBody.Email,
                Password = EncryptionHelper.EncryptPassword(registerBody.Password),
                RecuringDays = "",
                AttendanceIds = new List<int>(),
                Event_Attendances = new List<Event_Attendance>()
            };
            if (await _userStorage.Create(newUser))
            {
                return Ok("Registered Successfully");
            }
        }
        return BadRequest("Registration Failed");
    }

    [HttpGet("Read")]
    public async Task<IActionResult> Read([FromQuery] int? userId)
    {
        if (userId is null && HttpContext.Session.GetString("LoggedInUser") is null)
        return BadRequest("No user found");

        int id = userId ?? int.Parse(HttpContext.Session.GetString("LoggedInUser"));
        var user = await _userStorage.Read(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromQuery] int userId, [FromBody] User user)
    {
        if (await _userStorage.Update(userId, user)) return Ok("User updated");
        return NotFound();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int userId)
    {
        if (await _userStorage.Delete(userId)) return Ok("User deleted");
        return NotFound("user not found");
    }

    [HttpGet("GetWorkAttendances")]
    public async Task<IActionResult> GetAttendancesByUser([FromQuery] int userId) {
        List<Attendance> found = await this._AttendanceStorage.GetAllByUser(userId);
        return Ok(found);
    }

    [HttpGet("GetEvents")]
    public async Task<IActionResult> GetEventAttendancesByUser([FromQuery] int userId) {
        List<Event_Attendance> event_Attendances = await this._EventAttendanceStorage.GetAllByUser(userId);
        List<Event> found = await this._EventStorage.GetAllByIds(event_Attendances.Select(_ => _.EventId).ToList());
        return Ok(found);
    }
}

