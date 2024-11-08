using WebCalendaar.Models;
using WebCalendaar.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/User")]
public class UserController : Controller
{
    private readonly ILoginService _loginService;
    public IUserStorage _userStorage;

    public UserController(ILoginService loginService, IUserStorage storage)
    {
        this._userStorage = storage;
        this._loginService = loginService;
    }

    [HttpPost("")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterBody registerBody)
    {
        // registerBody == Firstname, lastName, Email, Password
        RegisterStatus RegisterState = await _loginService.RegisterUserAsync(registerBody);
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
                Password = registerBody.Password,
                RecuringDays = "",
                AttendanceIds = new List<int>(),
                Event_Attendances = new List<Event_Attendance>()
            };
            if (await _userStorage.Create(newUser))
            {
                return Ok("Registered Successfully");
            }
            return BadRequest("Registration Failed Cant create user");

        }
        else if (RegisterState == RegisterStatus.DuplicateEmail)
        {
            return BadRequest("That E-mail is already registerd");
        }
        else if (RegisterState == RegisterStatus.InvalidEmailFormat)
        {
            return BadRequest("Invalid E-mail format");
        }
        else if (RegisterState == RegisterStatus.InvalidPassword)
        {
            return BadRequest("Invalid Password");
        }
        return BadRequest("Registration Failed");
    }

    [HttpGet("Read")]
    public async Task<IActionResult> Read([FromQuery] int userId)
    {
        var user = await _userStorage.Read(userId);
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
}

