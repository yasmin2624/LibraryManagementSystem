using LibraryManagementSystem.Web.Models;
using LibraryManagementSystem.Web.Models.Books;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Books;

public class CreateModel : PageModel
{
    private readonly ApiService _apiService;

    public CreateModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public CreateBookDto Book { get; set; } = new();

    public List<AuthorDto> Authors { get; set; } = new();

    public List<CategoryDto> Categories { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        await LoadDataAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDataAsync();

            return Page();
        }

        var success = await _apiService.PostAsync(
            "api/Books",
            Book);

        if (!success)
        {
            ModelState.AddModelError(
                string.Empty,
                "Unable to create the book.");

            await LoadDataAsync();

            return Page();
        }

        return RedirectToPage("Index");
    }

    private async Task LoadDataAsync()
    {
        var authors = await _apiService.GetAsync<List<AuthorDto>>(
            "api/Authors");

        var categories = await _apiService.GetAsync<List<CategoryDto>>(
            "api/Categories");

        Authors = authors ?? new List<AuthorDto>();

        Categories = categories ?? new List<CategoryDto>();
    }
}