using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Data.Contexts;

public class DataContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<UserProfileEntity> UserProfiles { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
}
