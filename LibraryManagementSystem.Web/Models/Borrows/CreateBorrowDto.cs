using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Web.Models.Borrows
{
    public class CreateBorrowDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please select a book.")]
        public int BookID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a member.")]
        public int MemberID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a librarian.")]
        public int LibrarianID { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Borrowed";
    }
}