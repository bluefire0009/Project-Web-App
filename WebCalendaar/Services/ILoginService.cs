namespace WebCalendaar.Services;
using WebCalendaar.Models;

public interface ILoginService
{
    public Task<LoginStatus> CheckPasswordAsync(string username, string inputPassword, HttpContext context);
    public Task<RegisterStatus> RegisterUserAsync(RegisterBody registerBody);
}