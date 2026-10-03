namespace LibraryManagementSystem.Web.Models.Books;

public class BookDto
{
    public int BookID { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Genre { get; set; }
    public string? Language { get; set; }
    public bool Availability { get; set; }
    public int AuthorID { get; set; }
    public int CategoryID { get; set; }
}