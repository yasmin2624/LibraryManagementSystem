using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Web.Models.Category
{
    public class UpdateCategoryDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }
    }
}