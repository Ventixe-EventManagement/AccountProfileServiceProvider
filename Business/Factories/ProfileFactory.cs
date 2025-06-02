using Business.Models;
using Data.Entities;

namespace Business.Factories;
public static class ProfileFactory
{
    public static ProfileEntity ToEntity(ProfileRequest request)
    {
        return new ProfileEntity
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber
        };
    }

    public static ProfileRequest ToDto(ProfileEntity entity)
    {
        return new ProfileRequest
        {
            UserId = entity.UserId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            PhoneNumber = entity.PhoneNumber
        };
    }
}