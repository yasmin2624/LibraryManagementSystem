using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Borrow
{
    public class CreateBorrowDto
    {
        [Range(1, int.MaxValue)]
        public int BookID { get; set; }

        [Range(1, int.MaxValue)]
        public int MemberID { get; set; }

        [Range(1, int.MaxValue)]
        public int LibrarianID { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;
    }
}
