using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class UserProfileEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public int? AddressId { get; set; }
        public AddressEntity? Address { get; set; }
    }
}