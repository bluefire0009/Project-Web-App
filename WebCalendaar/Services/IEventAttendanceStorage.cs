public interface IEventAttendanceStorage {
    Task Create(EventAttendance eventAttendance);
    Task<EventAttendance?> Find(Guid id);
    Task Update(EventAttendance eventAttendance);
    Task Delete(Guid id);
}