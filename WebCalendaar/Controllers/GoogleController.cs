using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;
using System.Text;
using System.Text.Json;

[Route("api/google")]
public class GoogleController : Controller
{
    readonly IEventStorage _eventStorage;
    
    private readonly IEventAttendanceStorage _eventAttendanceStorage;

    public GoogleController(IEventStorage eventStorage, IEventAttendanceStorage eventAttendanceStorage)
    {
        _eventStorage = eventStorage;
        _eventAttendanceStorage = eventAttendanceStorage;
    }

    [HttpGet("syncAll")]
    public async Task<IActionResult> SyncAll()
    {
        List<Event_Attendance> event_Attendances = await this._eventAttendanceStorage.GetAllByUser(4);
        List<Event> allEvents = await this._eventStorage.GetAllUpcomingByIds(event_Attendances.Select(_ => _.EventId).ToList());

        var googleCalendarEvents = allEvents.Select(e => new
        {
            summary = e.Title,
            location = e.Location,
            description = e.Description,
            start = new
            {
                dateTime = $"{e.EventDate.ToString("yyyy-MM-dd")}T{e.StartTime}+00:00",
            },
            end = new
            {
                dateTime = $"{e.EventDate.ToString("yyyy-MM-dd")}T{e.EndTime}+00:00",
            },
            // recurrence = new[]
            // {
            //     new { freq = "daily", interval = 1, count = 5 }
            // },
            // attendees = new[]
            // {
            //     new { email = "attendee1@example.com" },
            //     new { email = "attendee2@example.com" }
            // },
            reminders = new
            {
                useDefault = false,
                // new[]
                // {
                //     new { method = "popup", minutes = 10 },
                //     new { method = "email", minutes = 1440 }
                // }
            }
        }).ToList();
        
        foreach (var googleCalendarEvent in googleCalendarEvents)
        {
            if (!await SendToGoogleCalendar(JsonSerializer.Serialize(googleCalendarEvent))) {
                return BadRequest("something went wrong");
            }
        }

        return Ok("success");
    }

    [HttpGet("sync/{eventId}")]
    public async Task<IActionResult> SyncAll(int eventId)
    {
        Event Event = await this._eventStorage.Read(eventId);

        var googleCalendarEvent = new
        {
            summary = Event.Title,
            location = Event.Location,
            description = Event.Description,
            start = new
            {
                dateTime = $"{Event.EventDate.ToString("yyyy-MM-dd")}T{Event.StartTime}+00:00",
            },
            end = new
            {
                dateTime = $"{Event.EventDate.ToString("yyyy-MM-dd")}T{Event.EndTime}+00:00",
            },
            // recurrence = new[]
            // {
            //     new { freq = "daily", interval = 1, count = 5 }
            // },
            // attendees = new[]
            // {
            //     new { email = "attendee1@example.com" },
            //     new { email = "attendee2@example.com" }
            // },
            reminders = new
            {
                useDefault = false,
                // new[]
                // {
                //     new { method = "popup", minutes = 10 },
                //     new { method = "email", minutes = 1440 }
                // }
            }
        };
        
        if (!await SendToGoogleCalendar(JsonSerializer.Serialize(googleCalendarEvent))) {
            return BadRequest("something went wrong");
        }

        return Ok("success");
    }

    private async Task<bool> SendToGoogleCalendar(string jsonData) {
        HttpClient client = new HttpClient();
        // api key header
        client.DefaultRequestHeaders.Add("key", "insert api key here");

        // OAuth key header
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "insert auth key here");

        // send request
        HttpContent postContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var result = await client.PostAsync("https://www.googleapis.com/calendar/v3/calendars/primary/events", postContent);
        return result.StatusCode == System.Net.HttpStatusCode.OK;
    }
}