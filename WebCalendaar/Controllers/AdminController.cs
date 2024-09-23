using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;
[Route("api/Admin")]
public class AdminController : Controller
{
    [HttpPost("Add")]
    public async Task<IActionResult> AddAdmin([FromBody] Admin admin)
    {
        return Ok();
    }

    [HttpPost("Get")]
    public async Task<IActionResult> GetAdmin([FromQuery] Guid id)
    {
        return Ok();
    }

    [HttpPost("Put")]
    public async Task<IActionResult> UpdateAdmin([FromBody] Admin admin,[FromQuery] Guid idToUpdate)
    {
        return Ok();
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> DeleteAdmin([FromQuery] Guid idToDelete)
    {
        return Ok();
    }
}