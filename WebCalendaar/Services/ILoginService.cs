namespace WebCalendaar.Services;

public interface ILoginService {
    public LoginStatus CheckPassword(string username, string inputPassword);
}