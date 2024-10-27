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
    List<Event> GetAll();
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
        return await db.Event.FirstOrDefaultAsync(x => x.EventId == event_id);
    }

        public async Task<bool> Update(int event_id, Event UpdatedEvent)
    {
        // find event in db
        Event? foundEvent = await this.Read(event_id);
        // return false if event doesnt exist
        if (foundEvent == null) return false;

        // update all the fields of the event
        foundEvent.Title = UpdatedEvent.Title;
        foundEvent.Description = UpdatedEvent.Description;
        foundEvent.EventDate = UpdatedEvent.EventDate;
        foundEvent.StartTime = UpdatedEvent.StartTime;
        foundEvent.EndTime = UpdatedEvent.EndTime;
        foundEvent.Location = UpdatedEvent.Location;
        foundEvent.AdminApproval = UpdatedEvent.AdminApproval;

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

    public List<Event> GetAll()
    {
        return db.Event.ToList();
    }
}