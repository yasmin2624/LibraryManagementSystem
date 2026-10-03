using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int RoleID { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; } = false;

        public bool EmailConfirmed { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public Role Role { get; set; } = null!;

        public ICollection<Borrow> MemberBorrows { get; set; } = new List<Borrow>();

        public ICollection<Borrow> LibrarianBorrows { get; set; } = new List<Borrow>();
    }
}
