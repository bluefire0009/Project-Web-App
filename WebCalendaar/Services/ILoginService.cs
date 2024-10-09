namespace WebCalendaar.Services;

public interface ILoginService
{
    public Task<LoginStatus> CheckPasswordAsync(string username, string inputPassword);
}