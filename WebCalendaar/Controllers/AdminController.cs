using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebCalendaar.Models;
[Route("api/Admin")]
public class AdminController : Controller
{
    readonly IAdminStorage adminStorage;

    public AdminController(IAdminStorage adminStorage)
    {
        this.adminStorage = adminStorage;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddAdmin([FromBody] Admin admin)
    {
        await adminStorage.Create(admin);
        return Created($"Created Admin: \n",admin);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetAdmin([FromQuery] Guid id)
    {
        Admin found = await adminStorage.Find(id);
        return Ok(found);
    }

    [HttpPut("Put")]
    public async Task<IActionResult> UpdateAdmin([FromBody] Admin admin,[FromQuery] Guid idToUpdate)
    {
        await adminStorage.Delete(idToUpdate);
        await adminStorage.Create(admin);
        return Created($"Updated Admin with Id={idToUpdate} to: \n",admin);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAdmin([FromQuery] Guid idToDelete)
    {
        await adminStorage.Delete(idToDelete);
        return Ok($"Deleted the Admin with Id={idToDelete}");
    }
}