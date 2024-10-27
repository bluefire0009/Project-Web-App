using Microsoft.AspNetCore.Authorization; // For auth attributes
using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

[Route("api/v1/events")]
public class EventController : Controller
{
    readonly IEventStorage _storage;

    public EventController(IEventStorage storage)
    {
        _storage = storage;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        var @event = await _storage.Read(id);
        if (@event == null)
        {
            return NotFound($"Event with id {id} not found");
        }

        /*var detailedEvent = new
        {
            @event.EventId,
            @event.Title,
            @event.Description,
            @event.EventDate,
            @event.StartTime,
            @event.EndTime,
            @event.Location,
            Attendees = @event.Event_AttendancesIds.Select(ea => new 
            {
                ea.User.UserId,
                ea.User.FirstName,
                ea.User.LastName
            }).ToList(),
            Reviews = @event.Event_Attendances.Select(ea => new 
            {
                ea.Rating,
                ea.Feedback
            }).ToList()
        };*/

        return Ok(@event);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateEvent([FromBody] Event @event)
    {
        if (@event == null)
        {
            return BadRequest("Event cannot be null");
        }

        bool success = await _storage.Create(@event);
        if (!success)
        {
            return BadRequest($"An event with id {@event.EventId} already exists");
        }

        return CreatedAtAction(nameof(GetEvent), new { id = @event.EventId }, @event);
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event @event)
    {
        if (@event == null)
        {
            return BadRequest("Event cannot be null");
        }

        bool success = await _storage.Update(id, @event);
        if (!success)
        {
            return NotFound($"Event with id {id} not found");
        }

        return Ok($"Event with id {id} updated successfully");
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        bool success = await _storage.Delete(id);
        if (!success)
        {
            return NotFound($"Event with id {id} not found");
        }

        return Ok($"Event with id {id} deleted successfully");
    }
}