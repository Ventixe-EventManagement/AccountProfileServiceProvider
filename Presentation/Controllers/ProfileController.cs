using System.Security.Claims;
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
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized("Ingen giltig användare.");

        request.UserId = userId;

        await _service.CreateOrUpdateProfileAsync(request);
        return Ok();
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetOwnProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var profile = await _service.GetProfileAsync(userId);
        return profile is not null ? Ok(profile) : NotFound();
    }

    [HttpGet("exists")]
    public async Task<IActionResult> Exists()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var exists = await _service.ProfileExistsAsync(userId);
        return Ok(new { exists });
    }
}
