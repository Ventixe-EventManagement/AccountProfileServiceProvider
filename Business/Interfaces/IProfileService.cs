using Business.Models;

namespace Business.Interfaces;
public interface IProfileService
{
    Task<bool> CreateOrUpdateProfileAsync(ProfileRequest request);
    Task<ProfileRequest?> GetProfileAsync(string userId);
}