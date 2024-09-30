using WebCalendaar.Models;
using Microsoft.AspNetCore.Mvc;




[Route("api/User")]
public class UserController : Controller
{
    public IUserStorage storage;

    public UserController(IUserStorage storage)
    {
        this.storage = storage;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        if (user == null) return BadRequest($"{user} from body is null");
        if (await storage.Create(user)) return Ok("user created");
        return BadRequest();
    }

    [HttpGet("Read")]
    public async Task<IActionResult> Read([FromQuery] Guid userId)
    {
        var user = await storage.Read(userId);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromQuery] Guid userId, [FromBody] User user)
    {
        if (await storage.Update(userId, user)) return Ok("User updated");
        return NotFound();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid userId)
    {
        if (await storage.Delete(userId)) return Ok("User deleted");
        return NotFound("user not found");
    }

}

