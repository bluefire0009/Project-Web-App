public class EventAttendance {
    public Guid Id {get; set;} = Guid.NewGuid();
    public Guid User_id {get; set;}
    public Guid Event_id {get; set;}

    public int Rating {get; set;}

    public string Feedback {get; set;}

    public EventAttendance(Guid user_id, Guid event_id, int rating, string feedback) {
        User_id = user_id;
        Event_id = event_id;
        Rating = rating;
        Feedback = feedback;
    }
}