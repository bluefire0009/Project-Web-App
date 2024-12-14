namespace WebCalendaar.Services;
using WebCalendaar.Models;

public interface ILoginService
{
    public Task<LoginStatus> CheckUserAsync(string username, string inputPassword, HttpContext context);
    public Task<RegisterStatus> CheckIfCanRegisterUserAsync(RegisterBody registerBody);
}