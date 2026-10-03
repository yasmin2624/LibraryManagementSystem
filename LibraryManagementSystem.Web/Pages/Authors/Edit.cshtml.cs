using LibraryManagementSystem.Web.Models;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Authors;

public class EditModel : PageModel
{
    private readonly ApiService _apiService;

    public EditModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public EditAuthorDto Author { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var result = await _apiService.GetAsync<AuthorDto>(
            $"api/Authors/{id}");

        if (result == null)
            return NotFound();

        Author = new EditAuthorDto
        {
            Name = result.Name,
            Email = result.Email,
            Nationality = result.Nationality
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
            return Page();

        var updated = await _apiService.PutAsync(
            $"api/Authors/{id}",
            Author);

        if (!updated)
            return NotFound();

        return RedirectToPage("Index");
    }
}