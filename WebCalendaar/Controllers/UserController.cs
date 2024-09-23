using WebCalendaar.Models;
using Microsoft.AspNetCore.Mvc;

public interface IUserController
{
    Task Create(User user);
    Task<User?> Read(int user_id);
    Task Update(int user_id, User user);
    Task Delete(int user_id);
}


[Route("api/User")]
public class UserController : IUserController
{
    [HttpPost("Create")]
    public Task Create(User user)
    {
        throw new NotImplementedException();
    }

    [HttpGet("Read")]
    public Task<User?> Read(int user_id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("Update")]
    public Task Update(int user_id, User user)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("Delete")]
    public Task Delete(int user_id)
    {
        throw new NotImplementedException();
    }

}

