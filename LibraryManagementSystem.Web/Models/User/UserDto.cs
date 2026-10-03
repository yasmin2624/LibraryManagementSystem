using System;

namespace LibraryManagementSystem.Web.Models.User
{
    public class UserDto
    {
        public int UserID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int RoleID { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}