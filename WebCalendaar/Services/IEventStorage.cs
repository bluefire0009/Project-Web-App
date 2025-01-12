using WebCalendaar.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public interface IEventStorage
{
    Task<bool> Create(Event @event); // Return bool
    Task<Event?> Read(int event_id); // Return int
    Task<bool> Update(int event_id, Event updatedEvent); // Returns bool
    Task<bool> Delete(int event_id); // Return int
    Task<List<Event>> GetAll();
    Task<List<Event>> GetAllUpcomingByIds(List<int> ids);
}

public class EventDBStorage : IEventStorage
{
    public DatabaseContext db;
    public EventDBStorage(DatabaseContext db)
    {
        this.db = db;
    }
    public async Task<bool> Create(Event @event)
    {
        if (await db.Event.AnyAsync(x => x.EventId == @event.EventId)) return false;
        await db.Event.AddAsync(@event);
        if (await db.SaveChangesAsync() > 0) return true;
        return false;
    }

    public async Task<Event?> Read(int event_id)
    {
        Event? eventInDatabase = await db.Event.FirstOrDefaultAsync(a => a.EventId == event_id);
        if (eventInDatabase == null)
            return null;

        return eventInDatabase;
    }

    public async Task<bool> Update(int event_id, Event UpdatedEvent)
    {
        // find event in db
        Event? foundEvent = await db.Event.FirstOrDefaultAsync(a => a.EventId == event_id);
        if (foundEvent == null)
            return false;

        // update all the fields of the event
        db.Event.Remove(foundEvent);
        await db.SaveChangesAsync();

        foundEvent.EventId = UpdatedEvent.EventId;
        foundEvent.Title = UpdatedEvent.Title;
        foundEvent.Description = UpdatedEvent.Description;
        foundEvent.EventDate = UpdatedEvent.EventDate;
        foundEvent.StartTime = UpdatedEvent.StartTime;
        foundEvent.EndTime = UpdatedEvent.EndTime;
        foundEvent.Location = UpdatedEvent.Location;
        foundEvent.AdminApproval = UpdatedEvent.AdminApproval;
        db.Event.Add(foundEvent);

        if (await db.SaveChangesAsync() > 0) return true;
        return false;

    }
    public async Task<bool> Delete(int @event_id)
    {
        // returns true if event was deleted
        Event? @event = await db.Event.FirstOrDefaultAsync(x => x.EventId == @event_id);
        if (@event == null) return false;
        db.Event.Remove(@event);
        if (await db.SaveChangesAsync() > 0) return true;
        return false;

    }

    public Task<List<Event>> GetAll()
    {
        return db.Event.ToListAsync();
    }

    public async Task<List<Event>> GetAllUpcomingByIds(List<int> ids)
    {
        List<Event> data = db.Event.Where(_ => ids.Contains(_.EventId)).ToList();
        return data.Where(_ => _.EventDate > DateOnly.FromDateTime(DateTime.Now)).ToList();
    }
}