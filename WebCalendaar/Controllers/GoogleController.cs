using Microsoft.AspNetCore.Authorization; // For auth attributes
using Microsoft.AspNetCore.Mvc;
using WebCalendaar.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Identity.Client.Extensions.Msal;

[Route("api/google")]
public class GoogleController : Controller
{
    [HttpGet("syncAll")]
    public async Task<IActionResult> SyncAll()
    {
        return Ok();
    }
}