public class Event_Attendance
{
    public Guid Id { get; set; }
    public Guid _user_id { get; set; }
    public string _event_id { get; set; }
    public List<float> _rating { get; private set; }
    public List<string> _feedBack { get; private set; }

    public Event_Attendance(Guid user_id, string event_id, List<float> rating, List<string> feedBack)
    {
        Id = Guid.NewGuid();
        _user_id = user_id;
        _event_id = event_id;
        _rating = rating;
        _feedBack = feedBack;
    }
}