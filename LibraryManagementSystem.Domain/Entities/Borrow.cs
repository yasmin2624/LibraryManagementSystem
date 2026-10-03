using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Borrow
    {
        public int BorrowID { get; set; }

        public int BookID { get; set; }

        public int MemberID { get; set; }

        public int LibrarianID { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public Book Book { get; set; } = null!;

        public User Member { get; set; } = null!;

        public User Librarian { get; set; } = null!;

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
