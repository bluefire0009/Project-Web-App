using WebCalendaar.Models;
using System;
using System.Threading.Tasks;

public interface IEventStorage
{
    Task Create(Event @event);
    Task<Event?> Find(Guid id);
    Task Update(Event @event);
    Task Delete(Guid id);
}