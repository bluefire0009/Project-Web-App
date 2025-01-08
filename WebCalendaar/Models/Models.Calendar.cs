using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCalendaar.Models
{
    public class User
    {
        public int UserId { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        // A comma sepparated string that could look like this: "mo,tu,we,th,fr"
        public required string RecuringDays { get; set; }

        public required List<int> AttendanceIds { get; set; }

        public required List<Event_Attendance> Event_Attendances { get; set; }
    }

    public class Attendance
    {
        public required DateOnly AttendanceDate { get; set; }
        public required int UserId { get; set; }
        public virtual User? User { get; set; }
    }

    public class Event_Attendance
    {
        [Key]
        public int Event_AttendanceId { get; set; }
        public int Rating { get; set; }
        public required string Feedback { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int UserId {get; set;}

        [ForeignKey("EventId")]
        public Event? Event { get; set; }
        public int EventId {get; set;}
    }

    public class Event
    {
        public int EventId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateOnly EventDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public required string Location { get; set; }

        public bool AdminApproval { get; set; }
    }
}