public class Attendance
{
    public Guid Id { get; set; }
    public string _user_id { get; private set; }
    public DateOnly _date { get; private set; }

    public Attendance(string user_id, DateOnly email)
    {
        Id = Guid.NewGuid();
        _user_id = user_id;
        _date = email;
    }
}