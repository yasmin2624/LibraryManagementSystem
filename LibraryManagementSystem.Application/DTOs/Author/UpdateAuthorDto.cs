using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Author
{
    public class UpdateAuthorDto
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? Nationality { get; set; }
    }
}
