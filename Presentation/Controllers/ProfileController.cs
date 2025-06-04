using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController(IProfileService service) : ControllerBase
{
    private readonly IProfileService _service = service;

    [HttpPost("upsert")]
    public async Task<IActionResult> Upsert(ProfileRequest request)
    {
        var userId = User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized("Ingen giltig användare.");

        request.UserId = userId;

        await _service.CreateOrUpdateProfileAsync(request);
        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(string userId)
    {
        var profile = await _service.GetProfileAsync(userId);
        return profile is not null ? Ok(profile) : NotFound();
    }

    [HttpGet("exists")]
    public async Task<IActionResult> Exists([FromQuery] string email)
    {
        var exists = await _service.ProfileExistsAsync(email);
        return Ok(new { exists });
    }
}
