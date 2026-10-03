namespace LibraryManagementSystem.Web.Models;

public class CategoryDto
{
    public int CategoryID { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}