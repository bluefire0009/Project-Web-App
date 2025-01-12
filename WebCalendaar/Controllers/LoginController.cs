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
        if (IsAdminLoggedIn() || IsUserLoggedIn())
        {
            return BadRequest($"You are already logged in as {HttpContext.Session.GetString("LoggedInUser")}{HttpContext.Session.GetString("LoggedInAdmin")}");
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
            HttpContext.Session.SetString("LoggedInAdmin", $"{loginBody.Username}");
            return Ok("Admin login success");
        }
        else if (LoginState == LoginStatus.IncorrectPassword) return Unauthorized("Incorrect password");
        else if (LoginState == LoginStatus.IncorrectUsername) return Unauthorized("Incorrect username");
        return BadRequest();
    }


    [HttpGet("IsAdminLoggedIn")]
    public bool IsAdminLoggedIn() => HttpContext.Session.GetString("AdminSession") == "LoggedIn";

    [HttpGet("IsUserLoggedIn")]
    public bool IsUserLoggedIn() => HttpContext.Session.GetString("UserSession") == "LoggedIn";

    [HttpGet("GetUserId")]
    public int GetUserId() => HttpContext.Session.GetString("UserSession") == "LoggedIn" ? int.Parse(HttpContext.Session.GetString("LoggedInUser")) : -1;


    [HttpGet("IsSessionRegisterd")]
    public IActionResult IsSessionRegisterd()
    {    // Retrieve session values
        bool Loggedin = HttpContext.Session.GetString("AdminSession") == "LoggedIn" || HttpContext.Session.GetString("UserSession") == "LoggedIn";
        if (Loggedin) return Ok("logged in");
        else return Ok("Not logged in");
    }


    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        if (IsAdminLoggedIn())
        {
            HttpContext.Session.SetString("AdminSession", "LoggedOut");
            HttpContext.Session.SetString("LoggedInAdmin", "");
            return Ok("Admin Logged out");
        }
        else if (IsUserLoggedIn())
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
