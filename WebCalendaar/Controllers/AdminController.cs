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
        if (admin == null){
            return BadRequest("Null in the request");
        }
        await adminStorage.Create(admin);
        return Created($"Created Admin: ",admin);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetAdmin([FromQuery] Guid id)
    {
        if (adminStorage.Find(id).Result == null){
            return NotFound($"Id : {id} not in the database");
        }
        Admin found = await adminStorage.Find(id);
        return Ok(found);
    }

    [HttpPut("Put")]
    public async Task<IActionResult> UpdateAdmin([FromBody] Admin admin,[FromQuery] Guid idToUpdate)
    {
        if (admin == null){
            return BadRequest("Null in the request");
        } else if (adminStorage.Find(idToUpdate).Result == null){
            return NotFound($"Id : {idToUpdate} not in the database");
        }
        await adminStorage.Delete(idToUpdate);
        await adminStorage.Create(admin);
        return Created($"Updated Admin with Id={idToUpdate} to: ",admin);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAdmin([FromQuery] Guid idToDelete)
    {
        if (adminStorage.Find(idToDelete).Result == null){
            return NotFound($"Id : {idToDelete} not in the database");
        }
        await adminStorage.Delete(idToDelete);
        return Ok($"Deleted the Admin with Id={idToDelete}");
    }
}