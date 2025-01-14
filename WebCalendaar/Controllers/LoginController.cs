using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Services;
using WebCalendaar.Models;

namespace WebCalendaar.Controllers;


[Route("api/")]
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
        if (IsAnyoneLoggedIn())
        {
            return BadRequest($"You are already logged in as {HttpContext.Session.GetString("LoggedInUser")}");
        }

        var LoginState = await _loginService.CheckUserAsync(loginBody.Username!, loginBody.Password!, HttpContext);
        if (LoginState == LoginStatus.Success)
        {
            HttpContext.Session.SetString("UserSession", "LoggedIn");
            HttpContext.Session.SetString("LoggedInUser", loginBody.Username!);

            return Ok($"login success as {loginBody.Username}");
        }
        else if (LoginState == LoginStatus.adminLoggedIn)
        {
            HttpContext.Session.SetString("AdminSession", "LoggedIn");
            HttpContext.Session.SetString("LoggedInUser", $"{loginBody.Username}");
            return Ok("Admin login success");
        }
        else if (LoginState == LoginStatus.IncorrectPassword) return Unauthorized("Incorrect password");
        else if (LoginState == LoginStatus.IncorrectUsername) return Unauthorized("Incorrect username");
        return BadRequest();
    }


    [HttpGet("IsAdminLoggedIn")]
    public IActionResult IsAdminLoggedIn()
    {
        bool isAdminLoggedIn = HttpContext.Session.GetString("AdminSession") == "LoggedIn";
        if (isAdminLoggedIn)
            return Ok("Admin is logged in");
        else
            return BadRequest("Admin is not logged in");
    }

    [HttpGet("IsUserLoggedIn")]
    public IActionResult IsUserLoggedIn()
    {
        bool isUserLoggedIn = HttpContext.Session.GetString("UserSession") == "LoggedIn";
        if (isUserLoggedIn)
            return Ok("User is logged in");
        else
            return BadRequest("User is not logged in");
    }

    [HttpGet("GetUserId")]
    public async Task<int> GetUserId() => HttpContext.Session.GetString("UserSession") == "LoggedIn" ? (await _userStorage.ReadByEmail(HttpContext.Session.GetString("LoggedInUser"))).UserId : -1;

    [HttpGet("GetUserName")]
    public IActionResult GetUserName() => Ok(HttpContext.Session.GetString("LoggedInUser"));

    [HttpGet("IsSessionRegisterd")]
    public IActionResult IsSessionRegisterd()
    {    // Retrieve session values
        bool Loggedin = HttpContext.Session.GetString("AdminSession") == "LoggedIn" || HttpContext.Session.GetString("UserSession") == "LoggedIn";
        if (Loggedin) return Ok("logged in");
        else return BadRequest("Not logged in");
    }

    public bool IsAnyoneLoggedIn()
    {
        return HttpContext.Session.GetString("AdminSession") == "LoggedIn" || HttpContext.Session.GetString("UserSession") == "LoggedIn";
    }

    public bool IsAdminLoggedIn1() => HttpContext.Session.GetString("AdminSession") == "LoggedIn";

    public bool IsUserLoggedIn1() => HttpContext.Session.GetString("UserSession") == "LoggedIn";


    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        if (IsAdminLoggedIn1())
        {
            HttpContext.Session.SetString("AdminSession", "LoggedOut");
            HttpContext.Session.SetString("LoggedInUser", "");
            return Ok("Admin Logged out");
        }
        else if (IsUserLoggedIn1())
        {
            HttpContext.Session.SetString("UserSession", "LoggedOut");
            HttpContext.Session.SetString("LoggedInUser", "");
            return Ok("User Logged out");
        }
        return BadRequest("You are not logged in");
    }
}

public class LoginBody
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
