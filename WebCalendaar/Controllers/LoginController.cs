using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Services;
using WebCalendaar.Models;

namespace WebCalendaar.Controllers;


[Route("api/v1/Login")]
public class LoginController : Controller
{
    private readonly ILoginService _loginService;
    public IUserStorage _userStorage;



    public LoginController(ILoginService loginService, IUserStorage userStorage)
    {
        _loginService = loginService;
        _userStorage = userStorage;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginBody loginBody)
    {
        if (loginBody is null)
        {
            return BadRequest("Loginbody is null");
        }
        if (IsSessionRegisterd() || IsUserLoggedIn())
        {
            return BadRequest($"You are already logged in as {HttpContext.Session.GetString("LoggedInUser")}{HttpContext.Session.GetString("LoggedInAdmin")}");
        }

        var LoginState = await _loginService.CheckPasswordAsync(loginBody.Username!, loginBody.Password!, HttpContext);
        if (LoginState == LoginStatus.Success)
        {
            HttpContext.Session.SetString("UserSession", "LoggedIn");
            HttpContext.Session.SetString("LoggedInUser", loginBody.Username!);
            return Ok($"login success as {loginBody.Username}");
        }
        else if (LoginState == LoginStatus.adminLoggedIn)
        {
            HttpContext.Session.SetString("AdminSession", "LoggedIn");
            HttpContext.Session.SetString("LoggedInAdmin", $"{loginBody.Username}");
            return Ok("Admin login success");
        }
        else if (LoginState == LoginStatus.IncorrectPassword) return Unauthorized("Incorrect password");
        else if (LoginState == LoginStatus.IncorrectUsername) return Unauthorized("Incorrect username");
        return BadRequest();
    }


    [HttpGet("IsAdminLoggedIn")]
    public IActionResult IsAdminLoggedIn()
    {
        // TODO: This method should return a status 200 OK when logged in, else 403, unauthorized : Done
        if (IsSessionRegisterd()) return Ok($"Logged in as {HttpContext.Session.GetString("LoggedInAdmin")}");
        return Unauthorized("You are not logged in As Admin");
    }
    public bool IsUserLoggedIn() => HttpContext.Session.GetString("UserSession") == "LoggedIn";
    public bool IsSessionRegisterd() => HttpContext.Session.GetString("AdminSession") == "LoggedIn";

    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        if (IsSessionRegisterd())
        {
            HttpContext.Session.SetString("AdminSession", "LoggedOut");
            return Ok("Admin Logged out");
        }
        else if (IsUserLoggedIn())
        {
            HttpContext.Session.SetString("UserSession", "LoggedOut");
            return Ok("User Logged out");
        }
        return BadRequest("You are not logged in");
    }
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterBody registerBody)
    {
        // registerBody == Firstname, lastName, Email, Password
        RegisterStatus RegisterState = await _loginService.RegisterUserAsync(registerBody);
        if (RegisterState == RegisterStatus.Success)
        {
            User newUser = new User
            {
                UserId = 0,
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
}

public class LoginBody
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
