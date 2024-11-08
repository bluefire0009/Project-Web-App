using Microsoft.EntityFrameworkCore;
using WebCalendaar.Models;
using WebCalendaar.Utils;

namespace WebCalendaar.Services;


public enum LoginStatus { IncorrectPassword, IncorrectUsername, Success, adminLoggedIn }

public enum RegisterStatus { Success, InvalidEmailFormat, DuplicateEmail, InvalidPassword }

// public enum ADMIN_SESSION_KEY { adminLoggedIn }

public class LoginService : ILoginService
{

    private readonly DatabaseContext _context;

    public LoginService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<LoginStatus> CheckUserAsync(string username, string inputPassword, HttpContext context)
    {

        // check first if the credentials match a user
        var passwordSha = EncryptionHelper.EncryptPassword(inputPassword);
        if (await _context.User.AnyAsync(x => x.Email == username && x.Password == passwordSha))
        {
            User? User = await _context.User.FirstOrDefaultAsync(x => x.Email == username && x.Password == passwordSha);
            context.Session.SetString("LoggedInUser", $"{User!.UserId}");
            return LoginStatus.Success;
        }
        else if (await _context.User.AnyAsync(x => x.Email == username && x.Password != passwordSha))
        {
            return LoginStatus.IncorrectPassword;
        }

        // then check if credentials match admin
        if (await _context.Admin.AnyAsync(x => x.UserName == username && x.Password == passwordSha))
        {
            return LoginStatus.adminLoggedIn;
        }
        else if (await _context.Admin.AnyAsync(x => x.UserName == username && x.Password != passwordSha))
        {
            return LoginStatus.IncorrectPassword;
        }
        return LoginStatus.IncorrectUsername;
    }

    public async Task<RegisterStatus> CheckIfCanRegisterUserAsync(RegisterBody registerBody)
    {
        if (await _context.User.AnyAsync(x => x.Email == registerBody.Email))
        {
            return RegisterStatus.DuplicateEmail;
        }
        else if (!IsValidEmail(registerBody.Email))
        {
            return RegisterStatus.InvalidEmailFormat;
        }
        // ------------------------------------
        // Add a more complex password checker here later
        else if (registerBody.Password.Length < 8)
        {
            return RegisterStatus.InvalidPassword;
        }
        // ------------------------------------
        return RegisterStatus.Success;
    }

    private bool IsValidEmail(string emailaddress)
    {
        try
        {
            System.Net.Mail.MailAddress MailAddress = new(emailaddress);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}