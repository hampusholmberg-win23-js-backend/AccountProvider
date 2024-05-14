using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ApplicationUser : IdentityUser
{
    public bool IsAdmin { get; set; } = false;
    public string UserProfileId { get; set; } = null!;
    public virtual UserProfileEntity UserProfile { get; set; } = new UserProfileEntity();

}
