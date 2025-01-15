using WebCalendaar.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Interface for event storage operations
public interface IEventStorage
{
    Task<bool> Create(Event @event); // Creates a new event, returns true if successful
    Task<Event?> Read(int event_id); // Retrieves a specific event by ID, returns null if not found
    Task<bool> Update(int event_id, Event updatedEvent); // Updates an existing event, returns true if successful
    Task<bool> Delete(int event_id); // Deletes an event by ID, returns true if successful
    Task<List<Event>> GetAll(); // Retrieves a list of all events
    Task<List<Event>> GetAllUpcomingByIds(List<int> ids); // Retrieves a list of upcoming events filtered by IDs
}

// Implementation of IEventStorage using a database context
public class EventDBStorage : IEventStorage
{
    public DatabaseContext db; // Database context for accessing the database

    // Constructor to inject the database context
    public EventDBStorage(DatabaseContext db)
    {
        this.db = db;
    }

    // Creates a new event in the database
    public async Task<bool> Create(Event @event)
    {
        // Check if an event with the same ID already exists
        if (await db.Event.AnyAsync(x => x.EventId == @event.EventId)) return false;

        // Add the event to the database
        await db.Event.AddAsync(@event);

        // Save changes and return true if successful
        if (await db.SaveChangesAsync() > 0) return true;
        return false;
    }

    // Reads a specific event by ID
    public async Task<Event?> Read(int event_id)
    {
        // Find the event in the database
        Event? eventInDatabase = await db.Event.FirstOrDefaultAsync(a => a.EventId == event_id);

        // Return the event or null if not found
        if (eventInDatabase == null)
            return null;

        return eventInDatabase;
    }

    // Updates an existing event
    public async Task<bool> Update(int event_id, Event UpdatedEvent)
    {
        // Find the existing event in the database
        Event? foundEvent = await db.Event.FirstOrDefaultAsync(a => a.EventId == event_id);
        if (foundEvent == null)
            return false;

        // Remove the old event record and save changes
        db.Event.Remove(foundEvent);
        await db.SaveChangesAsync();

        // Update all fields with the new event details
        foundEvent.EventId = UpdatedEvent.EventId;
        foundEvent.Title = UpdatedEvent.Title;
        foundEvent.Description = UpdatedEvent.Description;
        foundEvent.EventDate = UpdatedEvent.EventDate;
        foundEvent.StartTime = UpdatedEvent.StartTime;
        foundEvent.EndTime = UpdatedEvent.EndTime;
        foundEvent.Location = UpdatedEvent.Location;
        foundEvent.AdminApproval = UpdatedEvent.AdminApproval;

        // Add the updated event back to the database
        db.Event.Add(foundEvent);

        // Save changes and return true if successful
        if (await db.SaveChangesAsync() > 0) return true;
        return false;
    }

    // Deletes an event by ID
    public async Task<bool> Delete(int @event_id)
    {
        // Find the event in the database
        Event? @event = await db.Event.FirstOrDefaultAsync(x => x.EventId == @event_id);
        if (@event == null) return false;

        // Remove the event and save changes
        db.Event.Remove(@event);
        if (await db.SaveChangesAsync() > 0) return true;
        return false;
    }

    // Retrieves all events from the database
    public Task<List<Event>> GetAll()
    {
        return db.Event.ToListAsync();
    }

    // Retrieves upcoming events filtered by a list of IDs
    public async Task<List<Event>> GetAllUpcomingByIds(List<int> ids)
    {
        // Get all events matching the provided IDs
        List<Event> data = db.Event.Where(_ => ids.Contains(_.EventId)).ToList();

        // Filter events to only include those with a future date
        return data.Where(_ => _.EventDate > DateOnly.FromDateTime(DateTime.Now)).ToList();
    }
}
