using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;
using System.Text;
using System.Text.Json;

[Route("api/google")]
public class GoogleController : Controller
{
    [HttpGet("syncAll")]
    public async Task<IActionResult> SyncAll()
    {
        HttpClient client = new HttpClient();
        // api key header
        client.DefaultRequestHeaders.Add("key", "");

        // OAuth key header
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "");

        // event Json
        string jsonData = "{\"summary\":\"SampleEvent\",\"location\":\"123ExampleSt,ExampleCity,EX\",\"description\":\"Adetaileddescriptionoftheevent\",\"start\":{\"dateTime\":\"2025-01-09T09:00:00-07:00\",\"timeZone\":\"America/Los_Angeles\"},\"end\":{\"dateTime\":\"2025-01-09T10:00:00-07:00\",\"timeZone\":\"America/Los_Angeles\"},\"recurrence\":[{\"freq\":\"daily\",\"interval\":1,\"count\":5}],\"attendees\":[{\"email\":\"attendee1@example.com\"},{\"email\":\"attendee2@example.com\"}],\"reminders\":{\"useDefault\":false,\"overrides\":[{\"method\":\"popup\",\"minutes\":10},{\"method\":\"email\",\"minutes\":1440}]}}";

        // send request
        HttpContent postContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var result = client.PostAsync("https://www.googleapis.com/calendar/v3/calendars/primary/events", postContent).GetAwaiter().GetResult();
        return Ok(result);
    }
}