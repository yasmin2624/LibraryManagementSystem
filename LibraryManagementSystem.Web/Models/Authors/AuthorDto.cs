namespace LibraryManagementSystem.Web.Models;

public class AuthorDto
{
    public int AuthorID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
}