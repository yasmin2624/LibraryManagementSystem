using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Web.Models.Borrows
{
    public class UpdateBorrowDto
    {
        [Required]
        public DateTime DueDate { get; set; }
    }
}