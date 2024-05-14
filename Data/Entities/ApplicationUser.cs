using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class ApplicationUser : IdentityUser
{
    public bool IsAdmin { get; set; } = false;
    public int UserProfileId { get; set; }
    public virtual UserProfileEntity UserProfile { get; set; } = null!;

}
