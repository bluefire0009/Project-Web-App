namespace WebCalendaar.Models;

public class RegisterBody
{
    public required string Fullname { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}