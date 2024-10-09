using Microsoft.EntityFrameworkCore;
using WebCalendaar.Models;
using WebCalendaar.Utils;

namespace WebCalendaar.Services;


public enum LoginStatus { IncorrectPassword, IncorrectUsername, Success, adminLoggedIn }

// public enum ADMIN_SESSION_KEY { adminLoggedIn }

public class LoginService : ILoginService
{

    private readonly DatabaseContext _context;

    public LoginService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<LoginStatus> CheckPasswordAsync(string username, string inputPassword)
    {
        // check first if the credentials match a user
        if (await _context.User.AnyAsync(x => x.Email == username && x.Password == inputPassword))
        {
            return LoginStatus.Success;
        }
        else if (await _context.User.AnyAsync(x => x.Email == username && x.Password != inputPassword))
        {
            return LoginStatus.IncorrectPassword;
        }

        // then check if credentials match admin
        if (await _context.Admin.AnyAsync(x => x.UserName == username && x.Password == inputPassword))
        {
            return LoginStatus.adminLoggedIn;
        }
        else if (await _context.Admin.AnyAsync(x => x.UserName == username && x.Password != inputPassword))
        {
            return LoginStatus.IncorrectPassword;
        }
        return LoginStatus.IncorrectUsername;
    }
}