namespace LibraryManagementSystem.Web.Models;

public class CreateAuthorDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
}