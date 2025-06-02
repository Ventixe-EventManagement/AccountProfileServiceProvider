using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Context;

namespace Business.Services;
public class ProfileService(ProfileContext context) : IProfileService
{
    private readonly ProfileContext _context = context;

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

    public async Task<ProfileRequest?> GetProfileAsync(string userId)
    {
        var entity = await _context.Profiles.FindAsync(userId);
        return entity is not null ? ProfileFactory.ToDto(entity) : null;
    }
}