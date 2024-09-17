public class Event
{
    public Guid Id { get; set; }
    public string _tittle { get; private set; }
    public string _description { get; private set; }
    public DateOnly _date { get; private set; }
    public TimeOnly _start_time { get; private set; }
    public TimeOnly _end_time { get; private set; }
    public string _location { get; private set; }
    public bool _admin_approval { get; private set; }

    public Event(string tittle, string description, DateOnly date, TimeOnly start_time, TimeOnly end_time, string location, bool admin_approval)
    {
        Id = Guid.NewGuid();
        _tittle = tittle;
        _description = description;
        _date = date;
        _start_time = start_time;
        _end_time = end_time;
        _location = location;
        _admin_approval = admin_approval;
    }
}