using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Book
{
    public class UpdateBookDto
    {
        [Required]
        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Genre { get; set; }

        [StringLength(100)]
        public string? Language { get; set; }

        public bool Availability { get; set; }

        [Range(1, int.MaxValue)]
        public int AuthorID { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryID { get; set; }
    }
}
