using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class ProfileService(ProfileContext context) : IProfileService
{
    private readonly ProfileContext _context = context;

    // Creates a new profile or updates an existing one
    public async Task<bool> CreateOrUpdateProfileAsync(ProfileRequest request)
    {
        var existing = await _context.Profiles.FindAsync(request.UserId);

        if (existing is null)
        {
            var entity = ProfileFactory.ToEntity(request);
            _context.Profiles.Add(entity);
        }
        else
        {
            existing.FirstName = request.FirstName;
            existing.LastName = request.LastName;
            existing.PhoneNumber = request.PhoneNumber;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    // Retrieves a profile by user ID and maps it to a DTO
    public async Task<ProfileRequest?> GetProfileAsync(string userId)
    {
        var entity = await _context.Profiles.FindAsync(userId);

        return entity is not null ? ProfileFactory.ToDto(entity) : null;
    }

    // Checks if a profile exists for a given user ID
    public async Task<bool> ProfileExistsAsync(string userId)
    {
        return await _context.Profiles.AnyAsync(p => p.UserId == userId);
    }
}