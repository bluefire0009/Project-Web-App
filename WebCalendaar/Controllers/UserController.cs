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

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterBody registerBody)
    {
        // registerBody == Firstname, lastName, Email, Password
        RegisterStatus RegisterState = await _loginService.CheckIfCanRegisterUserAsync(registerBody);
        if (RegisterState == RegisterStatus.DuplicateEmail)
            return BadRequest("This email is already registered");
        if (RegisterState == RegisterStatus.InvalidEmailFormat)
            return BadRequest("This email has wrong format");
        if (RegisterState == RegisterStatus.InvalidPassword)
            return BadRequest("Password is too short");

        if (RegisterState == RegisterStatus.Success)
        {
            var fullname = registerBody.Fullname.Split();
            string fName = fullname[0];
            string lName = fullname.Length > 1 ? fullname[1] : "";
            User newUser = new User
            {
                FirstName = fName,
                LastName = lName,
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

    [HttpGet("All")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userStorage.GetAll();

        // Select only relevant fields
        var result = users.Select(user => new
        {
            Name = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Id = user.UserId
        });

        return Ok(result);
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

    [HttpGet("GetUpcomingWorkAttendances")]
    public async Task<IActionResult> GetAttendancesByUser([FromQuery] int userId)
    {
        List<Attendance> found = await this._AttendanceStorage.GetAllUpcomingByUser(userId);
        return Ok(found);
    }

    [HttpGet("GetUpcomingEvents")]
    public async Task<IActionResult> GetUpcomingEventAttendancesByUser([FromQuery] int userId)
    {
        List<Event_Attendance> event_Attendances = await this._EventAttendanceStorage.GetAllByUser(userId);
        List<Event> found = await this._EventStorage.GetAllUpcomingByIds(event_Attendances.Select(_ => _.EventId).ToList());
        return Ok(found);
    }
}

