using System.Security.Claims;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requires the user to be authenticated
public class ProfileController(IProfileService service) : ControllerBase
{
    private readonly IProfileService _service = service;

    // Creates or updates the user's profile
    [HttpPost("upsert")]
    public async Task<IActionResult> Upsert(ProfileRequest request)
    {
        // Retrieve the user ID from the authenticated user's claims
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // If no valid user ID is found, return 401 Unauthorized
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("No valid user.");

        // Set the user ID on the request model
        request.UserId = userId;

        // Create or update the profile using the service
        await _service.CreateOrUpdateProfileAsync(request);

        // Return 200 OK if successful
        return Ok();
    }

    // Retrieves the currently authenticated user's profile
    [HttpGet("me")]
    public async Task<IActionResult> GetOwnProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var profile = await _service.GetProfileAsync(userId);

        return profile is not null ? Ok(profile) : NotFound();
    }

    // Checks whether the profile exists for the current user
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
