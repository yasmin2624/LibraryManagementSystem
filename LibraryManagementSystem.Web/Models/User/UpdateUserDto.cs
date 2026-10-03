using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Web.Models.User
{
    public class UpdateUserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(1, 3)]
        public int RoleID { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}