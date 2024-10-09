using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Services;

namespace WebCalendaar.Controllers;


[Route("api/v1/Login")]
public class LoginController : Controller
{
    private readonly ILoginService _loginService;


    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginBody loginBody)
    {
        if (loginBody is null)
        {
            return BadRequest("Loginbody is null");
        }

        var LoginState = await _loginService.CheckPasswordAsync(loginBody.Username!, loginBody.Password!);
        if (LoginState == LoginStatus.Success)
        {
            HttpContext.Session.SetString("UserSession", "LoggedIn");
            HttpContext.Session.SetString("LoggedInUser", loginBody.Username!);
            return Ok("login success");
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
            return Ok("Logged out");
        }
        return BadRequest("You are not logged in");
    }

}

public class LoginBody
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
