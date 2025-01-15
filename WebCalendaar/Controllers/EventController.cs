using Microsoft.AspNetCore.Authorization; // For authorization attributes
using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Identity.Client.Extensions.Msal;

// Controller to manage event-related actions
[Route("api/events")]
public class EventController : Controller
{
    // Dependency for event storage operations
    readonly IEventStorage _storage;

    // Constructor to inject the event storage dependency
    public EventController(IEventStorage storage)
    {
        _storage = storage;
    }

    // Retrieves all events
    [HttpGet("get/all")]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _storage.GetAll(); // Fetch all events
        return Ok(events); // Return the list of events
    }

    // Retrieves a specific event by its ID
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        var @event = await _storage.Read(id); // Fetch the event by ID
        if (@event == null)
        {
            // Return 404 if the event is not found
            return NotFound($"Event with id {id} not found");
        }

        /* Detailed event logic commented out for potential future use:
        var detailedEvent = new
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
        };
        */

        return Ok(@event); // Return the event details
    }

    // Creates a new event
    [HttpPost("create")]
    public async Task<IActionResult> CreateEvent([FromBody] Event @event)
    {
        if (@event == null)
        {
            // Return 400 if the input event is null
            return BadRequest("Event cannot be null");
        }

        // Attempt to create the event
        bool success = await _storage.Create(@event);
        if (!success)
        {
            // Return 400 if the event already exists
            return BadRequest($"An event with id {@event.EventId} already exists");
        }

        // Return 201 with the created event's details
        return CreatedAtAction(nameof(GetEvent), new { id = @event.EventId }, @event);
    }

    // Updates an existing event
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event @event)
    {
        if (@event == null)
        {
            // Return 400 if the input event is null
            return BadRequest("Event cannot be null");
        }
        if (await _storage.Read(id) == null)
        {
            // Return 404 if the event does not exist
            return NotFound($"Event with id {id} could not be found");
        }
        if (await _storage.Read(@event.EventId) != null)
        {
            // Check for conflicting event IDs
            if (id != @event.EventId)
            {
                return BadRequest($"An event with id {@event.EventId} already exists");
            }
        }

        // Attempt to update the event
        bool success = await _storage.Update(id, @event);
        if (!success)
        {
            // Return 400 if the update fails
            return BadRequest($"Event with id {id} could not be updated");
        }

        // Return 200 if the update is successful
        return Ok($"Event with id {id} updated successfully");
    }

    // Deletes an existing event
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        // Attempt to delete the event
        bool success = await _storage.Delete(id);
        if (!success)
        {
            // Return 404 if the event is not found
            return NotFound($"Event with id {id} not found");
        }

        // Return 200 if the deletion is successful
        return Ok($"Event with id {id} deleted successfully");
    }
}
