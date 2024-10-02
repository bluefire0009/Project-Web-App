using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;
using System;
using System.Threading.Tasks;

[Route("api/v1/events")]
public class EventController : Controller
{
    readonly IEventStorage _storage;

    public EventController(IEventStorage storage)
    {
        _storage = storage;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateEvent([FromBody] Event @event)
    {
        if (@event == null)
        {
            return BadRequest("Event cannot be null");
        }

        await _storage.Create(@event);
        return CreatedAtAction(nameof(GetEvent), new { id = @event.EventId }, @event);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetEvent([FromQuery] Guid id)
    {
        var @event = await _storage.Find(id);
        if (@event == null)
        {
            return NotFound($"Event with id {id} not found");
        }
        return Ok(@event);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateEvent([FromBody] Event @event)
    {
        if (@event == null)
        {
            return BadRequest("Event cannot be null");
        }

        await _storage.Update(@event);
        return Ok(@event);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteEvent([FromQuery] Guid id)
    {
        var existingEvent = await _storage.Find(id);
        if (existingEvent == null)
        {
            return NotFound($"Event with id {id} not found");
        }

        await _storage.Delete(id);
        return Ok($"Event with id {id} deleted successfully");
    }
}