using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class ProfileContext(DbContextOptions<ProfileContext> options) : DbContext(options)
{
    public DbSet<ProfileEntity> Profiles { get; set; } = null!;
}