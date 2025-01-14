using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;
using System.Text;
using System.Text.Json;

[Route("api/google")]
public class GoogleController : Controller
{
    readonly IEventStorage _eventStorage;
    private readonly IEventAttendanceStorage _eventAttendanceStorage;
    private readonly IUserStorage _userStorage;

    public GoogleController(IEventStorage eventStorage, IEventAttendanceStorage eventAttendanceStorage, IUserStorage userStorage)
    {
        _eventStorage = eventStorage;
        _eventAttendanceStorage = eventAttendanceStorage;
        _userStorage = userStorage;
    }

    [HttpGet("syncAll/{myUserId}")]
    public async Task<IActionResult> SyncAll(int myUserId)
    {
        // Cannot be null because the controller checks if user is logged in, and if user is logged in, they should have an id
        // int myUserId = int.Parse(HttpContext.Session.GetString("LoggedInUser"));
        List<Event_Attendance> event_Attendances = await this._eventAttendanceStorage.GetAllByUser(myUserId);
        List<Event> allEvents = await this._eventStorage.GetAllUpcomingByIds(event_Attendances.Select(_ => _.EventId).ToList());
        List<string> googleCalendarEvents = allEvents.Select(e => 
$@"{{
    ""summary"": ""{e.Title}"",
    ""location"": ""{e.Location}"",
    ""description"": ""{e.Description}"",
    ""start"": {{
        ""dateTime"": ""{$"{e.EventDate.ToString("yyyy-MM-dd")}T{e.StartTime}+00:00"}""
    }},
    ""end"": {{
        ""dateTime"": ""{$"{e.EventDate.ToString("yyyy-MM-dd")}T{e.EndTime}+00:00"}""
    }},
    ""recurrence"": [
        {{
            ""freq"": ""daily"",
            ""interval"": 1,
            ""count"": 5
        }}
    ],
    ""attendees"": [
    ],
    ""reminders"": {{
        ""useDefault"": false,
        ""overrides"": [
            {{
                ""method"": ""popup"",
                ""minutes"": 10
            }},
            {{
                ""method"": ""email"",
                ""minutes"": 1440
            }}
        ]
    }}
}}").ToList();

        foreach (var googleCalendarEvent in googleCalendarEvents)
        {
            if (!await SendToGoogleCalendar(googleCalendarEvent, myUserId)) {
                return BadRequest("something went wrong");
            }
        }

        return Ok("success");
    }

    [HttpGet("sync/{eventId}")]
    public async Task<IActionResult> Sync(int eventId)
    {
        int myUserId = HttpContext.Session.GetString("UserSession") == "LoggedIn" ? (await _userStorage.ReadByEmail(HttpContext.Session.GetString("LoggedInUser"))).UserId : -1;
        Event? Event = await this._eventStorage.Read(eventId);
        if (Event == null)
            return NotFound($"Event with id:{eventId} not found");

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
        
        if (!await SendToGoogleCalendar(JsonSerializer.Serialize(googleCalendarEvent), myUserId)) {
            return BadRequest("something went wrong");
        }

        return Ok($"Event with id:{eventId} synced to your google calendar");
    }

    private async Task<bool> SendToGoogleCalendar(string jsonData, int userId) {
        HttpClient client = new HttpClient();
        // api key header
        client.DefaultRequestHeaders.Add("key", "");

        // OAuth key header
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await GetAuthToken(userId));

        // send request
        HttpContent postContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var result = await client.PostAsync("https://www.googleapis.com/calendar/v3/calendars/primary/events", postContent);
        return result.StatusCode == System.Net.HttpStatusCode.OK;
    }

    private async Task<string> GetAuthToken(int userId) {
        User user = await _userStorage.Read(userId);
        if (user.auth_token == null /*|| user.auth_token_time < DateTime.Now.AddHours(-1)*/) {
            user.auth_token = await RefreshAccessToken(user.refresh_token);
            await _userStorage.Update(userId, user);
        }
        return user.auth_token;
    }

    private async Task<string> RefreshAccessToken(string refresh_token) {
        HttpClient client = new HttpClient();
        string tokenUrl = "https://oauth2.googleapis.com/token";

        var requestData = new
        {
            client_id = "",
            client_secret = "",
            refresh_token = refresh_token,
            grant_type = "refresh_token"
        };

        // Convert request data to JSON
        string requestJson = JsonSerializer.Serialize(requestData);
        var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        // Make the HTTP POST request
        var response = await client.PostAsync(tokenUrl, content);
        string responseJson = await response.Content.ReadAsStringAsync();
        var responseData = JsonSerializer.Deserialize<TokenResponse>(responseJson);

        return responseData.AccessToken;
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
